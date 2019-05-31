using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_ACTIVE_EP_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            //string packetString = "FE 08 45 85 00 00 00 00 00 02 02 01 C9";
            //ZToolPacket data = GetPacket(packetString);
            var bytes = new DoubleByte((ushort)MessageId.ZDO_ACTIVE_EP_RSP);
            var packet = new byte[]
            {
                ZToolMessage.START_BYTE,
                7, //payload length
                bytes.Msb,
                bytes.Lsb,
                1, //device Id
                1, //device Id
                0, //status
                11,// network
                0,
                1, //EP
                6 //EP value
                //TODO: Missing checksum
            };

            ZToolMessage data = GetPacket(packet);
            //Assert.Equal(packet, data.PacketString);
            ;
            ZigBeeApsFrame apsFrame = ZdoActiveEndpoint.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("00 00 00 00 02 02 01"), apsFrame.Payload);
        }
    }
}
