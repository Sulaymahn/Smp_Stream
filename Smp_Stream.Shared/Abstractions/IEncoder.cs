namespace Smp_Stream.Shared.Abstractions
{
    public interface IEncoder
    {
        byte[] Encode(byte[] data);
    }
}
