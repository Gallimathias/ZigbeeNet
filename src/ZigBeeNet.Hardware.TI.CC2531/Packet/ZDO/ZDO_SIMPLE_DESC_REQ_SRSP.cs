﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_SIMPLE_DESC_REQ_SRSP : ZToolMessage
    {
        public PacketStatus Status { get; private set; }

        public ZDO_SIMPLE_DESC_REQ_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte((ushort)MessageId.ZDO_SIMPLE_DESC_REQ_SRSP), framedata);
        }
    }
}
