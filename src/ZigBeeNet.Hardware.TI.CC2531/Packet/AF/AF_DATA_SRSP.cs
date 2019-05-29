using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    [PacketParsing(ZToolCMD.AF_DATA_SRSP)]
    public class AF_DATA_SRSP : ZToolPacket
    {
        /// <name>TI.ZPI2.AF_DATA_SRSP.Status</name>
        /// <summary>Status</summary>
        public int Status;

        /// <name>TI.ZPI2.AF_DATA_SRSP</name>
        /// <summary>Constructor</summary>
        public AF_DATA_SRSP()
        {
        }

        public AF_DATA_SRSP(byte[] framedata, int offset, int length)
        {
            Status = framedata[offset];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_DATA_SRSP), framedata, offset, length);
        }

        public override string ToString() => "AF_DATA_SRSP(Status=" + Status + ')';
    }
}
