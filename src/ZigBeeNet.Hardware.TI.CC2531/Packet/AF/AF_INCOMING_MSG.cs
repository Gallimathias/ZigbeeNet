using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This callback message is in response to incoming data to any of the registered endpoints on this device
    /// </summary>
    [PacketParsing(ZToolCMD.AF_INCOMING_MSG)]
    public class AF_INCOMING_MSG : ZToolPacket
    {
        /// <summary>
        /// Specifies the group ID of the device
        /// </summary>
        public DoubleByte GroupId { get; private set; }

        /// <summary>
        /// Specifies the cluster Id (only the LSB is used in V1.0 networks.)
        /// </summary>
        public DoubleByte ClusterId { get; private set; }

        /// <summary>
        /// Specifies the ZigBee network address of the source device sending the message
        /// </summary>
        public ZToolAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// Specifies the source endpoint of the message 
        /// </summary>
        public byte SrcEndpoint { get; private set; }

        /// <summary>
        /// Specifies the destination endpoint of the message 
        /// </summary>
        public byte DstEndpoint { get; private set; }

        /// <summary>
        /// Specifies if the message was a broadcast or not 
        /// </summary>
        public byte WasBroadcast { get; private set; }

        /// <summary>
        /// Indicates the link quality measured during reception 
        /// </summary>
        public byte LinkQuality { get; private set; }

        /// <summary>
        /// Specifies if the security is used or not
        /// </summary>
        public byte SecurityUse { get; private set; }

        /// <summary>
        /// Specifies the timestamp of the message
        /// </summary>
        public long TimeStamp { get; private set; }

        /// <summary>
        /// Specifies transaction sequence number of the message 
        /// </summary>
        public byte TransSeqNumber { get; private set; }

        /// <summary>
        /// Specifies the length of the data. 
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Contains 0 to 128 bytes of data. 

        /// </summary>
        public byte[] Data { get; private set; }

        public AF_INCOMING_MSG(byte[] framedata, int offset, int length)
        {
            GroupId = new DoubleByte(framedata[offset + 1], framedata[offset]);
            ClusterId = new DoubleByte(framedata[offset + 3], framedata[offset + 2]);
            SrcAddr = new ZToolAddress16(framedata[offset + 5], framedata[offset + 4]);
            SrcEndpoint = framedata[offset + 6];
            DstEndpoint = framedata[offset + 7];
            WasBroadcast = framedata[offset + 8];
            LinkQuality = framedata[offset + 9];
            SecurityUse = framedata[offset + 10];
            byte[] bytes = new byte[4];
            bytes[3] = framedata[offset + 11];
            bytes[2] = framedata[offset + 12];
            bytes[1] = framedata[offset + 13];
            bytes[0] = framedata[offset + 14];
            //TimeStamp = BitConverter.ToInt32(framedata, offset + 11);
            TimeStamp = BitConverter.ToInt32(bytes, 0);
            TransSeqNumber = framedata[offset + 15];
            Len = framedata[offset + 16];
            Data = new byte[Len];

            Buffer.BlockCopy(framedata, offset + 17, Data, 0, Len);
            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_INCOMING_MSG), framedata, offset, length);
        }
    }
}
