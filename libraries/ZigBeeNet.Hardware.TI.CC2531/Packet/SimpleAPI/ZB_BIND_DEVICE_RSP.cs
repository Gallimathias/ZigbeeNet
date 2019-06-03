﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_BIND_DEVICE_RSP : ZToolMessage
    {
        public ZB_BIND_DEVICE_RSP()
        {
            BuildPacket(new DoubleByte((ushort)MessageId.ZB_BIND_DEVICE_RSP), new byte[0]);
        }

        public ZB_BIND_DEVICE_RSP(byte[] framedata)
        {
            BuildPacket(new DoubleByte((ushort)MessageId.ZB_BIND_DEVICE_RSP), framedata);
        }
    }
}