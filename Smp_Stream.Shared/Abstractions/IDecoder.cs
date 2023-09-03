namespace Smp_Stream.Shared.Abstractions
{
    public interface IDecoder
    {
        byte[] Decode(byte[] data);
    }
}
