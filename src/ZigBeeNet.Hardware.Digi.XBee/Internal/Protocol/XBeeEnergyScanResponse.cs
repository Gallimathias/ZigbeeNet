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
    /// Class to implement the XBee command " Energy Scan ".
    /// AT Command <b>ED</b></p>Start an Energy Detect scan. This command accepts an argument to
    /// specify the time in milliseconds to scan IEEE 802.15.4 channels 11 through 26. The device
    /// loops through all 16 channels until the time elapses and returns the maximal energy on each
    /// channel. In Transparent mode, a comma must follow each value with the list ending with a
    /// carriage return. The values returned reflect the detected energy level in units of -dBm.
    /// Convert an ED response of 49, 3A, and so on, to decimal to become -73 dBm, -58 dBm, and so on. 
    /// This class provides methods for processing XBee API commands.
    /// </summary>
    public class XBeeEnergyScanResponse : XBeeFrame, IXBeeResponse 
    {
        
        /// <summary>
        /// Response field
        /// </summary>
        private int _frameId;
        
        /// <summary>
        /// Response field
        /// </summary>
        private CommandStatus _commandStatus;
        
        /// <summary>
        ///  Return the frameId as <see cref="System.Int32"/>
        /// </summary>
        public int GetFrameId()
        {
            return _frameId;
        }
        
        /// <summary>
        ///  Return the commandStatus as <see cref="CommandStatus"/>
        /// </summary>
        public CommandStatus GetCommandStatus()
        {
            return _commandStatus;
        }
        
        /// <summary>
        /// Method for deserializing the fields for the response </summary>
        public void Deserialize(int[] incomingData)
        {
            this.InitializeDeserializer(incomingData);
            this._frameId = this.DeserializeInt8();
            DeserializeAtCommand();
            this._commandStatus = this.DeserializeCommandStatus();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(382);
        }
    }
}
