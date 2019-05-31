﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    [PacketParsing(MessageId.SYS_TEST_LOOPBACK_SRSP)]
    public class SYS_TEST_LOOPBACK_SRSP : ZToolMessage
    {
        public byte[] TestData;

        public SYS_TEST_LOOPBACK_SRSP()
        {

        }

        public SYS_TEST_LOOPBACK_SRSP(byte[] framedata, int offset, int length)
        {
            this.TestData = new byte[length];
            Buffer.BlockCopy(framedata, offset, TestData, 0, length);
            BuildPacket(new DoubleByte((ushort)MessageId.SYS_TEST_LOOPBACK_SRSP), framedata, offset, length);
        }
    }
}
