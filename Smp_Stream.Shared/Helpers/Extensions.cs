using System;

namespace Smp_Stream.Shared.Helpers
{
    public static class Extensions
    {
        public static byte[] LogBytes(this byte[] bytes, string start)
        {
            Console.WriteLine($"{start}: [ {string.Join(", ", bytes)} ] : count: {bytes.Length}");
            return bytes;
        }
    }
}
