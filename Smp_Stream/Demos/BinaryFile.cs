using Smp_Stream.Shared.Abstractions;
using Smp_Stream.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stream = Smp_Stream.Shared.Abstractions.Stream;

namespace Smp_Stream.Desktop.Demos
{
    public class BinaryFile : Stream
    {
        public ICodec Codec { get; private set; }

        public BinaryFile(string filename) : base(filename)
        {
            Codec = new UnghostCodec(0x5555);
        }

        public override void Write(string data)
        {
            BaseStream.SetLength(0);

            //hello

            byte[] bytes = Encoding.UTF8.GetBytes(data);

            byte[] code = Codec.Encode(bytes);

            foreach (var @byte in code)
            {
                BaseStream.WriteByte(@byte);
            }
            BaseStream.Position = 0;
        }

        public override string Read()
        {
            BaseStream.Position = 0;
            var bytes = new List<byte>();
            int buffer = BaseStream.ReadByte();
            while (buffer != -1)
            {
                bytes.Add((byte)buffer);
                buffer = BaseStream.ReadByte();
            }

            byte[] code = Codec.Decode(bytes.ToArray());
            return Encoding.UTF8.GetString(code);
        }

        public override int ReadByte() => BaseStream.ReadByte();
        public override void WriteByte(byte value) => BaseStream.WriteByte(value);
    }
}
