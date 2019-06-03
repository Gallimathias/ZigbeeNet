using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    [PacketParsing(MessageId.ZDO_ACTIVE_EP_RSP)]
    public class ZDO_ACTIVE_EP_RSP : ZToolMessage
    {
        public ZToolAddress16 SrcAddr { get; private set; }

        public PacketStatus Status { get; private set; }

        public ZToolAddress16 NwkAddr { get; private set; }

        public byte ActiveEPCount { get; private set; }

        public byte[] ActiveEpList { get; private set; }

        public ZDO_ACTIVE_EP_RSP(byte[] framedata, int offset = 0, int length = -1)
        {
            SrcAddr = new ZToolAddress16(framedata[offset + 1], framedata[offset]);
            Status = (PacketStatus)framedata[offset + 2];
            NwkAddr = new ZToolAddress16(framedata[offset + 4], framedata[offset + 3]);

            ActiveEPCount = framedata[offset + 5];
            ActiveEpList = new byte[ActiveEPCount];
            Buffer.BlockCopy(framedata, offset + 6, ActiveEpList, 0, ActiveEPCount);

            BuildPacket(new DoubleByte((ushort)MessageId.ZDO_ACTIVE_EP_RSP), framedata, offset, length);
        }
    }
}
