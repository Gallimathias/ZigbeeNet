using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This command is used by tester to build and send a data request message
    /// </summary>
    public class AF_DATA_REQUEST_EXT : ZToolMessage
    {
        public override int Size => size;

        private readonly int size;

        public ushort GroupId { get; private set; }
        public short SrcEndPoint { get; private set; }
        public int J { get; private set; }
        public int K { get; private set; }
        public byte BitmapOpt { get; private set; }
        public byte Radius { get; private set; }
        public byte[] Payload { get; private set; }

        internal AF_DATA_REQUEST_EXT() : base(MessageId.AF_DATA_REQUEST_EXT) { } //For serialization and deserialization
        public AF_DATA_REQUEST_EXT(ushort groupdId, short srcEndPoint, int j, int k, byte bitmapOpt, byte radius,
                byte[] payload) : this()
        {

            if (payload.Length > 230)
            {
                throw new ArgumentException("Payload is too big, maxium is 230", nameof(payload));
            }

            size = payload.Length + 20;
            GroupId = groupdId;
            SrcEndPoint = srcEndPoint;
            J = j;
            K = k;
            BitmapOpt = bitmapOpt;
            Radius = radius;
            Payload = payload;

        }



        public override void DeSerialize(byte[] buffer, int offset)
        {
            //TODO deserialize raw payload to this message
        }

        public override void Serialize(byte[] buffer, int offset)
        {
            buffer[offset] = 0x01; // Destination address mode 1 (group addressing)
            buffer[offset + 1] = GroupId.GetByte(0); // Source address
            buffer[offset + 2] = GroupId.GetByte(1); // Source address
            buffer[offset + 3] = 0x00; // Source address
            buffer[offset + 4] = 0x00; // Source address
            buffer[offset + 5] = 0x00; // Source address
            buffer[offset + 6] = 0x00; // Source address
            buffer[offset + 7] = 0x00; // Source address
            buffer[offset + 8] = 0x00; // Source address
            buffer[offset + 9] = 0x00; // Destination Endpoint
            buffer[offset + 10] = 0x00; // Destination PAN ID
            buffer[offset + 11] = 0x00; // Destination PAN ID
            buffer[offset + 12] = (byte)(SrcEndPoint & 0xFF);
            buffer[offset + 13] = J.GetByte(0);
            buffer[offset + 14] = J.GetByte(1);
            buffer[offset + 15] = (byte)(K & 0xFF);
            buffer[offset + 16] = (byte)(BitmapOpt & 0xFF);
            buffer[offset + 17] = (byte)(Radius & 0xFF);
            buffer[offset + 18] = Payload.Length.GetByte(0);
            buffer[offset + 19] = Payload.Length.GetByte(1);

            offset += 20;

            for (int i = 0; i < Payload.Length; i++)
                buffer[offset + i] = Payload[i];
        }
    }
}