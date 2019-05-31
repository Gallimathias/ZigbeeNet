using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_PERMIT_JOINING_REQUEST_RSP : ZToolMessage
    {
        public byte Status { get; private set; }

        public ZB_PERMIT_JOINING_REQUEST_RSP(byte[] data)
        {
            Status = data[0];
            
            BuildPacket(new DoubleByte((ushort)MessageId.ZB_PERMIT_JOINING_REQUEST_RSP), data);
        }
    }
}
