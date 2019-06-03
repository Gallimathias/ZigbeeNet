using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using Serilog;
using ZigBeeNet.Transport;
using System.Reflection;
using System.Linq;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ZToolPacketParser
    {
        private static readonly Dictionary<MessageId, Type> parserDictionary;

        static ZToolPacketParser()
            => parserDictionary = Assembly
                    .GetAssembly(typeof(ZToolMessage))
                    .GetTypes()
                    .Where(t => typeof(ZToolMessage).IsAssignableFrom(t) && t.GetCustomAttribute<PacketParsingAttribute>() != null)
                    .ToDictionary(
                        t => t.GetCustomAttribute<PacketParsingAttribute>().CMD,
                        t => t);

        /// <summary>
        /// The packet handler.
        /// </summary>
        private IZToolPacketHandler _packetHandler;

        /// <summary>
        /// The input port.
        /// </summary>
        private IZigBeePort _port;

        /// <summary>
        /// The parser parserThread.
        /// </summary>
        private Task _parserTask = null;

        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Construct which sets input stream where the packet is read from the and handler
        /// which further processes the received packet.
        ///
        /// <param name="port">the <see cref="ZigBeePort"></param>
        /// <param name="packetHandler">the packet handler</param>
        /// </summary>
        public ZToolPacketParser(IZigBeePort port, IZToolPacketHandler packetHandler)
        {
            Log.Verbose("Creating ZToolPacketParser");

            _port = port;
            _cancellationToken = new CancellationTokenSource();
            _packetHandler = packetHandler;

            _parserTask = new Task(Run, TaskCreationOptions.LongRunning);
            _parserTask.Start(TaskScheduler.Default);
        }

        /// <summary>
        /// Run method executed by the parser thread.
        /// </summary>
        public void Run()
        {
            Log.Verbose("ZToolPacketParser parserThread started");
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    byte[] val = _port.Read();
                    if (val[0] == ZToolPacket.START_BYTE)
                    {
                        // inputStream.mark(256);
                        //var packetStream = new ZToolPacketStream(_port);
                        ZToolMessage response = ParsePacket(val, 0, val.Length);

                        Log.Verbose("Response is {Type} -> {Response}", response.GetType().Name, response);
                        if (response.Error) //TODO implement error message
                        {
                            Log.Debug("Received a BAD PACKET {Response}", response.ToString());
                            // inputStream.reset();
                            continue;
                        }

                        _packetHandler.HandlePacket(response);
                    }
                    else if (val != null)
                    {
                        // Log if not end of stream.
                        Log.Debug("Discarded stream: expected start byte but received {Value}", val);
                    }
                }
                catch (IOException e)
                {
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        _packetHandler.Error(e);
                        _cancellationToken.Cancel();
                    }
                }
            }
            Log.Debug("ZToolPacketParser parserThread exited.");
        }

        /// <summary>
        /// Requests parser thread to shutdown.
        /// </summary>
        public void Close()
        {
            _cancellationToken.Cancel();
        }

        /// <summary>
        /// Checks if parser thread is alive.
        ///
        /// <returns>true if parser thread is alive.</returns>
        /// </summary>
        public bool IsAlive()
        {
            return _parserTask != null && _parserTask.Status == TaskStatus.Running;
        }

        public static ZToolMessage GetMessageById(MessageId id)
        {
            if (parserDictionary.TryGetValue(id, out var type))
                return Activator.CreateInstance(type) as ZToolMessage;

            //TODO: Return generic message
            //TODO: implement generic message
            Log.Warning("Unknown command ID: {Command}", id);
            return new ZToolMessage(new DoubleByte((ushort)id), payload);
        }

        public static ZToolMessage ParsePacket(byte[] data, int offset, int dataLength)
        {
            if (dataLength < 5)
                throw new ArgumentException($"{nameof(dataLength)} is lower than 5", nameof(dataLength));

            if (offset < 0)
                throw new ArgumentException($"{nameof(offset)} is lower than 0", nameof(offset));

            if (data[offset] != ZToolPacket.START_BYTE)
                throw new ArgumentException($"{nameof(data)} is not valid {nameof(ZToolPacket)}", nameof(data));

            Exception exception;
            try
            {
                var packet = ZToolPacket.Parse(data, offset, dataLength);
                var response = GetMessageById((MessageId)packet.CMD.Value);
                response.DeSerialize(packet.Payload, 0);
                return response;
            }
            catch (Exception e)
            {
                Log.Error("Packet parsing failed due to exception.", e);
                exception = e;
            }
            ZToolMessage exceptionResponse = new ErrorPacket();

            if (exception != null)
            {
                exceptionResponse.Error = true; //TODO: implement error message
                exceptionResponse.ErrorMsg = exception.Message;
            }

            return exceptionResponse;
        }

    }
}
