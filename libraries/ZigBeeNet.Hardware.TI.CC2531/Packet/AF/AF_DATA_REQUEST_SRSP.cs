﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    public class AF_DATA_REQUEST_SRSP : ZToolMessage
    {
        public PacketStatus Status { get; private set; }

        public AF_DATA_REQUEST_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte((ushort)MessageId.AF_DATA_SRSP), framedata);
        }
    }
}