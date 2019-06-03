using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Tranport.SerialPort
{
    public class ZigBeeSerialPort : IZigBeePort
    {
        private System.IO.Ports.SerialPort _serialPort;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ConcurrentQueue<byte[]> _fifoBuffer;

        public string PortName { get; set; }
        public int Baudrate { get; set; }
        public bool IsOpen => _serialPort != null && _serialPort.IsOpen;

        public ZigBeeSerialPort(string portName, int baudrate = 115200)
        {
            PortName = portName;
            Baudrate = baudrate;

            _serialPort = new System.IO.Ports.SerialPort(portName, baudrate);
            _cancellationTokenSource = new CancellationTokenSource();
            _fifoBuffer = new ConcurrentQueue<byte[]>();
        }

        public void Close()
        {
            _cancellationTokenSource?.Cancel();

            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.Dispose();
                _serialPort = null;
            }
        }

        public bool Open()
        {
            try
            {
                return Open(115200);
            }
            catch (Exception e)
            {
                Log.Warning("Unable to open serial port: " + e.Message);
                return false;
            }
        }

        public bool Open(int baudrate)
        {
            Baudrate = baudrate;

            var success = false;

            Log.Debug("Opening port {Port} at {Baudrate} baud.", PortName, baudrate);

            _serialPort = new System.IO.Ports.SerialPort(PortName, baudrate);

            try
            {
                var tryOpen = true;

                if (!Environment.OSVersion.Platform.ToString().StartsWith("Win"))
                {
                    tryOpen = (tryOpen && File.Exists(PortName));
                }
                if (tryOpen)
                {
                    _serialPort.Open();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("{Exception} - Error opening port {Port}\n{Port}", ex.GetType().Name, PortName, ex.Message);
            }

            if (_serialPort.IsOpen)
            {
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();
                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.DataReceived += OnDataReceived;
                // Start Reader Task
                //_readerTask = new Task(ReaderTask, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);

                //_readerTask.Start(TaskScheduler.Default);

                // TODO: ConnectionStatusChanged event
            }

            return success;
        }

        public void PurgeRxBuffer()
        {
            /*
             *  The enumeration represents a moment-in-time snapshot of the contents of the queue. It does not reflect any updates to the collection after GetEnumerator was called.
             *  The enumerator is safe to use concurrently with reads from and writes to the queue.
             *  https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.getenumerator?view=netframework-4.8
             */
            while (_fifoBuffer.Count > 0)
            {
                _fifoBuffer.Clear();
            }
        }

        public byte[] Read() => Read(9999999);

        public byte[] Read(int timeout)
        {
            try
            {
                /* This blocks until data available (Producer Consumer pattern) */
                var notTimedOut = _fifoBuffer.TryDequeue(out var value);

                if (notTimedOut)
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while reading byte from serial port");
            }
            return null;
        }

        public void Write(byte[] value)
        {
            //If port is null or not open
            if (_serialPort == null || !IsOpen)
                return;

            try
            {
                _serialPort.Write(value, 0, value.Length);

                Log.Debug("Write data to serialport: {Data}", BitConverter.ToString(value));
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while writing to serial port");
            }
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                var length = _serialPort.BytesToRead;
                var message = new byte[length];
                var bytesRead = 0;
                var bytesToRead = length;

                do
                {
                    var n = _serialPort.Read(message, bytesRead, length - bytesRead); // read may return anything from 0 - length , 0 = end of stream

                    if (n == 0)
                        break;

                    bytesRead += n;
                    bytesToRead -= n;
                } while (bytesToRead > 0);

                _fifoBuffer.Enqueue(message);

            }
            catch (Exception exception)
            {
                Log.Error(exception, "Error while reading from serial port");
            }
        }
    }
}
