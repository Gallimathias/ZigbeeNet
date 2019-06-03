using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_FIND_DEVICE_REQUEST_RSP : ZToolMessage
    {
        public ZB_FIND_DEVICE_REQUEST_RSP()
        {
            BuildPacket(new DoubleByte((ushort)MessageId.ZB_FIND_DEVICE_REQUEST_RSP), new byte[0]);
        }

        public ZB_FIND_DEVICE_REQUEST_RSP(byte[] framedata)
        {
            BuildPacket(new DoubleByte((ushort)MessageId.ZB_FIND_DEVICE_REQUEST_RSP), framedata);
        }
    }
}
