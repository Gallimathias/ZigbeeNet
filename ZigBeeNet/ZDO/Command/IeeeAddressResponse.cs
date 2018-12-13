﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * IEEE Address Response value object class.
     * 
     * The IEEE_addr_rsp is generated by a Remote Device in response to an
     * IEEE_addr_req command inquiring as to the 64-bit IEEE address of the Remote
     * Device or the 64-bit IEEE address of an address held in a local discovery cache.
     * The destination addressing on this command shall be unicast.
    */
    public class IeeeAddressResponse : ZdoResponse
    {
        /**
         * IEEEAddrRemoteDev command message field.
         */
        public IeeeAddress IeeeAddrRemoteDev { get; set; }

        /**
         * NWKAddrRemoteDev command message field.
         */
        public ushort NwkAddrRemoteDev { get; set; }

        /**
         * StartIndex command message field.
         */
        public byte StartIndex { get; set; }

        /**
         * NWKAddrAssocDevList command message field.
         */
        public List<ushort> NwkAddrAssocDevList { get; set; }

        /**
         * Default constructor.
         */
        public IeeeAddressResponse()
        {
            ClusterId = 0x8001;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(IeeeAddrRemoteDev, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(NwkAddrRemoteDev, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(NwkAddrAssocDevList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            for (int cnt = 0; cnt < NwkAddrAssocDevList.Count; cnt++)
            {
                serializer.Serialize(NwkAddrAssocDevList[cnt], ZclDataType.Get(DataType.NWK_ADDRESS));
            }
        }
        public override void Deserialize(ZclFieldDeserializer deserializer)

        {
            base.Deserialize(deserializer);

            // Create lists
            NwkAddrAssocDevList = new List<ushort>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));

            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }

            IeeeAddrRemoteDev = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            NwkAddrRemoteDev = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));

            if (deserializer.IsEndOfStream)
            {
                return;
            }

            byte? numAssocDev = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (numAssocDev != null)
            {
                for (int cnt = 0; cnt < numAssocDev; cnt++)
                {
                    NwkAddrAssocDevList.Add((ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("IeeeAddressResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", ieeeAddrRemoteDev=")
                   .Append(IeeeAddrRemoteDev)
                   .Append(", nwkAddrRemoteDev=")
                   .Append(NwkAddrRemoteDev)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(", nwkAddrAssocDevList=")
                   .Append(NwkAddrAssocDevList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
