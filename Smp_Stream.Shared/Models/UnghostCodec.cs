using Smp_Stream.Shared.Abstractions;
using Smp_Stream.Shared.Helpers;

namespace Smp_Stream.Shared.Models
{
    public class UnghostCodec : ICodec
    {
        private readonly short _key;

        public byte[] Decode(byte[] data)
        {
            byte[] result = new byte[data.Length / 2];
            int resultIndex = 0;
            for (int i = 0; i < data.Length; i += 2)
            {
                var res = data[i] << 8 | data[i + 1];
                res ^= _key;
                result[resultIndex++] = (byte)(res >> 4);
            }
            return result.LogBytes("decoded: ");
        }

        public byte[] Encode(byte[] data)
        {
            byte[] result = new byte[data.Length * 2];
            int resultIndex = 0;
            for (int i = 0; i < data.Length; i++)
            {
                short res = (short)(data[i] << 4);
                res ^= _key;
                byte leftByte = (byte)(res >> 8);
                byte rightByte = (byte)(res & 0xFF);
                result[resultIndex++] = leftByte;
                result[resultIndex++] = rightByte;
            }
            return result.LogBytes("encoded: ");
        }

        public UnghostCodec(short key)
        {
            _key = key;
        }
    }
}
