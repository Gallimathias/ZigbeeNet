using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class PacketParsingAttribute : Attribute
    {
        public MessageId CMD { get; }

        public PacketParsingAttribute(MessageId cmd)
        {
            CMD = cmd;
        }

    }
}
