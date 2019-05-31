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
    public class ZToolPacket
    {
        public const int PAYLOAD_START_INDEX = 4;

        public const byte START_BYTE = 0xFE;

        public ushort CommandId => CMD.Value;

        public byte[] Payload { get; set; }

        public byte LEN { get; set; }

        public DoubleByte CMD { get; set; }

        public bool Error { get; set; } = false;

        public byte FCS { get; set; }

        public string ErrorMsg { get; set; }

        public CommandType Type { get; private set; }

        public CommandSubsystem Subsystem { get; private set; }
                

        /// <summary>
        /// I started off using bytes but quickly realized that java bytes are signed, so effectively only 7 bits.
        /// We should be able to use int instead.
        ///
        /// </summary> // PROTECTED?        
        public ZToolPacket()
        {
        }

        public byte[] GetBytes()
        {
            // packet size is start byte + len byte + 2 cmd bytes + data + checksum byte
            var packet = new byte[LEN + 5];
            packet[0] = START_BYTE;

            // note: if checksum is not correct, XBee won't send out packet or return error. ask me how I know.
            // checksum is always computed on pre-escaped packet
            Checksum checksum = new Checksum();
            // Packet length does not include escape bytes
            //LEN = length - 5;
            packet[1] = (byte)LEN;
            checksum.AddByte(packet[1]);
            // msb Cmd0 -> Type & Subsystem
            packet[2] = CMD.Msb;
            checksum.AddByte(packet[2]);
            // lsb Cmd1 -> PROFILE_ID_HOME_AUTOMATION
            packet[3] = CMD.Lsb;
            checksum.AddByte(packet[3]);

            Buffer.BlockCopy(Payload, 0, packet, PAYLOAD_START_INDEX, LEN);
            checksum.AddBytes(Payload, 0, Payload.Length);
            // set last byte as checksum
            FCS = checksum.Value;
            packet[packet.Length - 1] = FCS;

            return packet;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Packet: subsystem=")
               .Append(Subsystem)
               .Append(", length=")
               .Append(LEN)
               .Append(", apiId=")
               .Append(ByteUtils.ToBase16(CMD.Msb))
               .Append(" ")
               .Append(ByteUtils.ToBase16(CMD.Lsb))
               .Append(", data=")
               .Append(ByteUtils.ToBase16(Packet))
               .Append(", checksum=")
               .Append(ByteUtils.ToBase16(FCS))
               .Append(", error=")
               .Append(Error);
            if (Error)
            {
                builder.Append(", errorMessage=");
                builder.Append(ErrorMsg);
            }

            return builder.ToString();
        }

        public enum CommandType : byte
        {
            POLL = 0x00,
            /// <summary>
            /// Synchronous Messages A Synchronous Request (SREQ) is a frame, defined by data content instead of 
            /// the ordering of events of the physical interface, which is sent from the Host to NP where the 
            /// next frame sent from NP to Host must be the Synchronous Response (SRESP) to that SREQ. 
            /// 
            /// Note that once a SREQ is sent, the NPI interface blocks until a corresponding response(SRESP) is received.
            /// </summary>
            SREQ = 0x01,
            /// <summary>
            /// Asynchronous Messages Asynchronous Request – transfer initiated by Host Asynchronous Indication – transfer initiated by NP. 
            /// </summary>
            AREQ = 0x02,
            /// <summary>
            /// Synchronous Response
            /// </summary>
            SRSP = 0x03,
            RES0 = 0x04,
            RES1 = 0x05,
            RES2 = 0x06,
            RES3 = 0x07
        }

        public enum CommandSubsystem : byte
        {
            RES = 0x00,
            SYS = 0x01,
            MAC = 0x02,
            NWK = 0x03,
            AF = 0x04,
            ZDO = 0x05,
            SAPI = 0x06,
            UTIL = 0x07,
            DBG = 0x08,
            APP = 0x09,
            RCAF = 0x0a,
            RCN = 0x0b,
            RCN_CLIENT = 0x0c,
            BOOT = 0x0d,
            ZIPTEST = 0x0e,
            DEBUG = 0x0f,
            PERIPHERALS = 0x10,
            NFC = 0x11,
            PB_NWK_MGR = 0x12,
            PB_GW = 0x13,
            PB_OTA_MGR = 0x14,
            BLE_SPNP = 0x15,
            BLE_HCI = 0x16,
            RESV01 = 0x17,
            RESV02 = 0x18,
            RESV03 = 0x19,
            RESV04 = 0x1a,
            RESV05 = 0x1b,
            RESV06 = 0x1c,
            RESV07 = 0x1d,
            RESV08 = 0x1e,
            SRV_CTR = 0x1f
        }

        public static ZToolPacket Parse(byte[] frameData, int offset, int length)
        {
            if (frameData[offset] != START_BYTE)
                throw new ArgumentException("The given data does not match a ZToolPacket", nameof(frameData));

            var len = frameData[offset + 1];
            var idMsb = frameData[offset + 2];

            var packet = new ZToolPacket()
            {
                LEN = len,
                CMD = new DoubleByte(idMsb, frameData[offset + 3]),
                FCS = frameData[offset + len + 4],
                Subsystem = (CommandSubsystem)(idMsb & 0x1F),
                Type = (CommandType)((idMsb & 0x60) >> 5)
            };

            Buffer.BlockCopy(frameData, offset + 4, packet.Payload, 0, len);
            //TODO Checksum check
            //throw new ZToolParseException("Packet checksum failed");
            return packet;
        }
    }
}
