using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_PING : ZToolMessage
    {
        public SYS_PING()
        {
            BuildPacket(new DoubleByte((ushort)MessageId.SYS_PING), new byte[0]);
        }
    }
}
