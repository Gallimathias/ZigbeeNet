using System;
using System.IO;
using System.Linq;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class Cc2351TestPacket
    {
        protected byte[] GetPacketData(string stringData)
        {
            string hex = stringData.Replace(" ", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        protected ZToolMessage GetPacket(string stringData)
        {
            byte[] packet = GetPacketData(stringData);

            byte[] byteArray = new byte[packet.Length - 1];
            for (int c = 1; c < packet.Length; c++)
            {
                byteArray[c - 1] = (byte)packet[c];
            }

            return GetPacket(byteArray);
        }
        protected ZToolMessage GetPacket(byte[] byteArray)
        {           

            IZigBeePort port = new TestPort(byteArray, null);

            try
            {
                //ZToolPacket ztoolPacket = new ZToolPacketStream(port).ParsePacket();
                var array = port.Read();
                ZToolMessage ztoolPacket = ZToolPacketStream.ParsePacket(array, 0, array.Length);

                Assert.False(ztoolPacket.Error);

                return ztoolPacket;
            }
            catch (IOException e)
            {
                return null;
            }
        }


    }
}
