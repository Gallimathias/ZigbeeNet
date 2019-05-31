using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class TestPort : IZigBeePort
    {
        byte[] input;
        byte[] output;

        public TestPort(byte[] input, byte[] output)
        {
            this.input = input;
            this.output = output;
        }

        public bool Open()
        {
            return true;
        }

        public void Close()
        {
        }

        public void Write(byte[] value)
        {
        }



        public bool Open(int baudRate)
        {
            return false;
        }

        public bool Open(int baudRate, FlowControl flowControl)
        {
            return false;
        }

        public void PurgeRxBuffer()
        {
        }

        public byte[] Read()
                => input;

        public byte[] Read(int timeout)
            => Read();
    }
}
