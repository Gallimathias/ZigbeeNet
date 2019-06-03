using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    [PacketParsing(MessageId.AF_DATA_SRSP_EXT)]
    public class AF_DATA_SRSP_EXT : ZToolMessage
    {
        /// <summary>
        /// Response status.
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// Constructor which sets frame data.
        ///
        /// <param name="framedata">the frame data</param>
        /// </summary>
        public AF_DATA_SRSP_EXT(byte[] framedata, int offset, int length)
        {
            Status = framedata[offset];
            BuildPacket(new DoubleByte((ushort)MessageId.AF_DATA_SRSP_EXT), framedata, offset, length);
        }

        public override string ToString()
        {
            return "AF_DATA_SRSP_EXT(Status=" + Status + ')';
        }
    }
}
