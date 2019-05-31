using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    /// <summary>
    /// This callback is sent by the device to indicate that a reset has occurred. 
    /// </summary>
    [PacketParsing(MessageId.SYS_RESET_RESPONSE)]
    public class SYS_RESET_RESPONSE : ZToolMessage
    {
        /// <summary>
        /// Hardware revision number
        /// </summary>
        public byte HwRev { get; private set; }

        /// <summary>
        /// Major release number
        /// </summary>
        public byte MajorRel{ get; private set; }

        /// <summary>
        /// Minor release number
        /// </summary>
        public byte MinorRel { get; private set; }

        /// <summary>
        /// Product
        /// </summary>
        public byte Product { get; private set; }

        /// <summary>
        /// Reason for the reset
        /// </summary>
        public ResetType Reason { get; private set; }

        /// <summary>
        /// Transport protocol revision
        /// </summary>
        public byte TransportRev { get; private set; }

        public SYS_RESET_RESPONSE(byte[] framedata, int offset = 0, int length = -1)
        {
            Reason = (ResetType)framedata[offset];
            TransportRev = framedata[offset + 1];
            Product = framedata[offset + 2];
            MajorRel = framedata[offset + 3];
            MinorRel = framedata[offset + 4];
            HwRev = framedata[offset + 5];

            BuildPacket(new DoubleByte((ushort)MessageId.SYS_RESET_RESPONSE), framedata, offset, length);
        }

        public enum ResetType : byte
        {
            PowerUp = 0x00,
            External = 0x01,
            WatchDog = 0x02
        }
    }
}
