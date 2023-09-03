using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smp_Stream.Demos
{
    public class NetworkFileServer
    {
        TcpClient client { get; set; }

        public async Task<NetworkStream> ConnectAsync()
        {
            const int retryIntervalMs = 1000;
            while (true)
            {
                try
                {
                    await client.ConnectAsync("192.168.11.103", 7777);
                    return client.GetStream();
                }
                catch (SocketException)
                {
                    Console.WriteLine("Failed to connect. Retrying...");
                }

                await Task.Delay(retryIntervalMs);
            }
        }


        public NetworkFileServer()
        {
            client = new TcpClient();
        }
    }
}
