using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    [PacketParsing(MessageId.AF_REGISTER_SRSP)]
    public class AF_REGISTER_SRSP : ZToolMessage
    {
        public PacketStatus Status { get; private set; }

        public AF_REGISTER_SRSP(byte[] framedata, int offset, int length)
        {
            Status = (PacketStatus)framedata[offset];

            BuildPacket(new DoubleByte((ushort)MessageId.AF_REGISTER_SRSP), framedata, offset, length);
        }
    }
}
