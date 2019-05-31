using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using Serilog;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    public class SynchronousCommandListener : ISynchronousCommandListener
    {
        public event EventHandler<ZToolMessage> OnResponseReceived;

        public void ReceivedCommandResponse(ZToolMessage packet)
        {
            Log.Verbose(" {Packet} received as synchronous command.", packet.GetType().Name);
            OnResponseReceived?.Invoke(this, packet);
        }
    }
}
