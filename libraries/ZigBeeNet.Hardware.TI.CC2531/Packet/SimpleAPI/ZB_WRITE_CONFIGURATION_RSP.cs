using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_WRITE_CONFIGURATION_RSP : ZToolMessage
    {
        public PacketStatus Status { get; private set; }

        public ZB_WRITE_CONFIGURATION_RSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte((ushort)MessageId.ZB_WRITE_CONFIGURATION_RSP), framedata);
        }
    }
}
