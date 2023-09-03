using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stream = Smp_Stream.Shared.Abstractions.Stream;

namespace Smp_Stream.Desktop.Demos
{
    public class TextFile : Stream
    {
        public override string Read() =>
            File.ReadAllText(FilePath);

        public override void Write(string data)
        {
            File.WriteAllText(FilePath, data);
        }

        public override void WriteByte(byte value)
        {
            BaseStream.WriteByte(value);
        }

        public override int ReadByte() =>
            BaseStream.ReadByte();

        public TextFile(string filename) : base(filename)
        {
            BaseStream.Close();
        }
    }
}
