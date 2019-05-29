using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    [PacketParsing(ZToolCMD.AF_REGISTER_SRSP)]
    public class AF_REGISTER_SRSP : ZToolPacket
    {
        public PacketStatus Status { get; private set; }

        public AF_REGISTER_SRSP(byte[] framedata, int offset, int length)
        {
            Status = (PacketStatus)framedata[offset];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_REGISTER_SRSP), framedata, offset, length);
        }
    }
}
