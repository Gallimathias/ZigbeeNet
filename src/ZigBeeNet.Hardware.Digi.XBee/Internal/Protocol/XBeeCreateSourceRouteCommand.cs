//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    
    
    /// <summary>
    /// Class to implement the XBee command " Create Source Route ".
    /// This frame creates a source route in the device. A source route specifies the complete route a
    /// packet traverses to get from source to destination. For best results, use source routing
    /// with many-to-one routing. There is no response frame for this frame type. Take care when
    /// generating source routes. An incorrectly formatted frame will be silently rejected by the
    /// radio or cause unexpected results. 
    /// This class provides methods for processing XBee API commands.
    /// </summary>
    public class XBeeCreateSourceRouteCommand : XBeeFrame, IXBeeCommand 
    {
        
        /// <summary>
        ///  The frame Id 
        /// </summary>
        private int _frameId;
        
        /// <summary>
        ///  64-bit destination address. MSB first, LSB last. Set to the 64-bit address of the
        /// destination device. Reserved 64-bit address for the coordinator = 0x0000000000000000
        /// Broadcast = 0x000000000000FFFF. 
        /// </summary>
        private IeeeAddress _ieeeAddress;
        
        /// <summary>
        ///  16-bit destination network address. Set to the 16-bit address of the destination device, if
        /// known. If the address is unknown or if sending a broadcast, set to 0xFFFE. 
        /// </summary>
        private int _networkAddress;
        
        /// <summary>
        ///  </summary>
        private int[] _addressList;
        
        /// <summary>
        /// The frameId to set as </summary>
        /// <seecref="uint8"
        ///>
        ///  </see>
        public void SetFrameId(int frameId)
        {
            this._frameId = frameId;
        }
        
        /// <summary>
        /// The ieeeAddress to set as </summary>
        /// <seecref="IeeeAddress"
        ///>
        ///  </see>
        public void SetIeeeAddress(IeeeAddress ieeeAddress)
        {
            this._ieeeAddress = ieeeAddress;
        }
        
        /// <summary>
        /// The networkAddress to set as </summary>
        /// <seecref="uint16"
        ///>
        ///  </see>
        public void SetNetworkAddress(int networkAddress)
        {
            this._networkAddress = networkAddress;
        }
        
        /// <summary>
        /// The addressList to set as </summary>
        /// <seecref="uint16[]"
        ///>
        ///  </see>
        public void SetAddressList(int[] addressList)
        {
            this._addressList = addressList;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public int[] Serialize()
        {
            this.SerializeCommand(33);
            this.SerializeInt8(_frameId);
            this.SerializeIeeeAddress(_ieeeAddress);
            this.SerializeInt16(_networkAddress);
            this.SerializeInt8(0);
            this.SerializeInt8(_addressList.Length);
            this.SerializeInt16Array(_addressList);
            return this.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(658);
        }
    }
}
