using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    [PacketParsing(MessageId.SYS_RPC_ERROR)]
    public class SYS_RPC_ERROR : ZToolMessage
    {
        /// <name>TI.ZPI2.SYS_RPC_ERROR.ErrCmd0</name>
        /// <summary>Command byte 0 of the message causing an error.</summary>
        public int ErrCmd0 { get; set; }
        /// <name>TI.ZPI2.SYS_RPC_ERROR.ErrCmd1</name>
        /// <summary>Command byte 1 of the message causing an error.</summary>
        public int ErrCmd1 { get; set; }
        /// <name>TI.ZPI2.SYS_RPC_ERROR.Status</name>
        /// <summary>Status</summary>
        public int Status { get; set; }

        /// <name>TI.ZPI2.SYS_RPC_ERROR</name>
        /// <summary>Constructor</summary>
        public SYS_RPC_ERROR()
        {
        }

        /// <name>TI.ZPI2.SYS_RPC_ERROR</name>
        /// <summary>Constructor</summary>
        public SYS_RPC_ERROR(byte num1, byte num2, byte num3)
        {
            this.Status = num1;
            this.ErrCmd0 = num2;
            this.ErrCmd1 = num3;
            byte[] framedata = { num1, num2, num3 };
            BuildPacket(new DoubleByte((ushort)MessageId.SYS_RPC_ERROR), framedata, 0, framedata.Length);
        }

        public SYS_RPC_ERROR(byte[] framedata, int offset, int length)
        {
            this.Status = framedata[offset];
            this.ErrCmd0 = framedata[offset + 1];
            this.ErrCmd1 = framedata[offset + 3];
            BuildPacket(new DoubleByte((ushort)MessageId.SYS_RPC_ERROR), framedata, offset, length);
        }

        public override string ToString()
        {
            return "SYS_RPC_ERROR{" +
                    "ErrCmd0=" + ErrCmd0 +
                    ", ErrCmd1=" + ErrCmd1 +
                    ", Status=" + Status +
                    '}';
        }
    }
}
