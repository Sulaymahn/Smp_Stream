using Smp_Stream.Demos;

namespace Smp_Stream
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var server = new NetworkFileServer();
            var stream = new BinaryFile("demo.unghost");
            var nstream = await server.ConnectAsync();
            stream.Write("In the dimly lit room, the flickering candle cast eerie shadows on the walls. The old book lay open on the dusty wooden table, its pages filled with cryptic symbols and ancient incantations. As I gazed at the text, a shiver ran down my spine, for this was no ordinary manuscript; it was an unghostdude file, rumored to hold the secrets of a long-forgotten realm. The air grew heavy with anticipation, and I couldn't help but wonder what mysteries awaited me within the pages of this enigmatic tome.");

            var readByte = stream.ReadByte();
            while (readByte != -1)
            {
                nstream.WriteByte((byte)readByte);
                readByte = stream.ReadByte();
            }

            nstream.Close();
            Console.WriteLine($"sent: {stream.Read()}");
        }
    }
}