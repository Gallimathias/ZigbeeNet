using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class PacketParsingAttribute : Attribute
    {
        public ZToolCMD CMD { get; }

        public PacketParsingAttribute(ZToolCMD cmd)
        {
            CMD = cmd;
        }

    }
}
