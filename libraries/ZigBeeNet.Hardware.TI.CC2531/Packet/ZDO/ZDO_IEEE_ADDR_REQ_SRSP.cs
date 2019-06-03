using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_IEEE_ADDR_REQ_SRSP : ZToolMessage
    {
        public PacketStatus Status { get; private set; }

        public ZDO_IEEE_ADDR_REQ_SRSP(byte[] data)
        {
            Status = (PacketStatus)data[0];

            BuildPacket(new DoubleByte((ushort)MessageId.ZDO_IEEE_ADDR_REQ_SRSP), data);
        }
    }
}
