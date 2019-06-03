namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public interface ISerializeable
    {
        int Size { get; }

        void DeSerialize(byte[] buffer, int offset);
        void Serialize(byte[] buffer, int offset);
    }
}