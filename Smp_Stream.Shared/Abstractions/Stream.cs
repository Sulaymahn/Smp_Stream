using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smp_Stream.Shared.Abstractions
{
    public abstract class Stream
    {
        protected string FilePath { get; set; }
        protected FileStream BaseStream { get; set; }
        public abstract void Write(string data);
        public abstract void WriteByte(byte value);
        public abstract string Read();
        public abstract int ReadByte();
        public void Close() => BaseStream.Dispose();

        public Stream(string filename)
        {
            FilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "StreamDemo",
                filename);
            BaseStream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
    }
}
