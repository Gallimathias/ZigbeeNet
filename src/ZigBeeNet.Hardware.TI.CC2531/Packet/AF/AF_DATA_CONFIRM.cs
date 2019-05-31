using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This command is sent by the device to the user after it receives a data request. To confirm the data
    /// </summary>
    public class AF_DATA_CONFIRM : ZToolMessage
    {
        /// <summary>
        /// Endpoint of the device 
        /// </summary>
        public byte Endpoint { get; private set; }

        /// <summary>
        /// Status is either Success (0) or Failure (1). 
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// Specified the transaction sequence number of the message 
        /// </summary>
        public byte TransId { get; private set; }

        public override int Size => 3; //Status 1byte + endpoint 1byte + transid 1byte

        internal AF_DATA_CONFIRM() : base(MessageId.AF_DATA_CONFIRM) { } //For serialization and deserialization
        public AF_DATA_CONFIRM(byte endpoint, PacketStatus status, byte transId) 
            : this()            
        {
            Endpoint = endpoint;
            Status = status;
            TransId = transId;
        }

        public override void Serialize(byte[] buffer, int offset)
        {
            buffer[offset] = (byte)Status;
            buffer[offset + 1] = Endpoint;
            buffer[offset + 2] = TransId;
        }

        public override void DeSerialize(byte[] buffer, int offset)
        {
            Status = (PacketStatus)buffer[offset];
            Endpoint = buffer[offset + 1];
            TransId = buffer[offset + 2];
        }
    }
}
