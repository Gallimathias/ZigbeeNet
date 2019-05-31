using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Extensions;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public abstract class ZToolMessage : ISerializeable
    {
        public MessageId Id { get; }
        public abstract int Size { get; }

        public ZToolMessage(MessageId id)
        {
            Id = id;
        }

        public abstract void Serialize(byte[] buffer, int offset);

        public abstract void DeSerialize(byte[] buffer, int offset);
    }
}
