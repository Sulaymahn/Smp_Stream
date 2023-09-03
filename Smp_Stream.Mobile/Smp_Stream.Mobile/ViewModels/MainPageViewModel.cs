using Smp_Stream.Shared.Abstractions;
using Smp_Stream.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Smp_Stream.Mobile.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private string _letter;
        public string LastRecievedLetter
        {
            get => _letter;
            set
            {
                if (_letter != value)
                {
                    OnPropertyChanged();
                    _letter = value;
                }
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _rawdata;
        public string RawData
        {
            get => _rawdata;
            set
            {
                if (_rawdata != value)
                {
                    _rawdata = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _listenBGColor;
        public Color ListenBGColor
        {
            get => _listenBGColor;
            set
            {
                if (_listenBGColor != value)
                {
                    _listenBGColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IP { get => GetLocalIpAddress(); }

        public ICommand ClearCommand { get; set; }
        public ICommand ListenCommand { get; set; }

        public event EventHandler<int> ByteRecieved;
        private TcpListener _listener;

        public MainPageViewModel()
        {
            _listener = new TcpListener(IPAddress.Any, 7777);
            ClearCommand = new Command(ClearAsync);
            ListenCommand = new Command(async () => await ListenAsync());
            ListenBGColor = Color.Gray;
            ByteRecieved += OnByteRecieved;
        }

        private void OnByteRecieved(object sender, int e)
        {
            bytes.Add((byte)e);
            RawData += e.ToString();

            if (bytes.Count % 2 == 0)
            {
                Text = Encoding.UTF8.GetString(codec.Decode(bytes.ToArray()));
            }

            LastRecievedLetter = e.ToString();
        }

        void ClearAsync()
        {
            Text = string.Empty;
            RawData = string.Empty;
            LastRecievedLetter = string.Empty;
            bytes.Clear();
        }

        readonly List<byte> bytes = new List<byte>();
        readonly ICodec codec = new UnghostCodec(0x5555);

        public async Task ListenAsync()
        {
            ListenBGColor = Color.Salmon;
            _listener.Start();
            var rnd = new Random();
            while (true)
            {
                using (TcpClient client = await _listener.AcceptTcpClientAsync())
                using (NetworkStream stream = client.GetStream())
                {
                    try
                    {
                        int byteRead = stream.ReadByte();
                        while (byteRead != -1)
                        {
                            ByteRecieved?.Invoke(this, byteRead);
                            await Task.Delay(rnd.Next(20, 100));
                            byteRead = stream.ReadByte();
                        }
                    }
                    catch (Exception) { }
                }

            }
        }

        public static string GetLocalIpAddress()
        {
            string ipAddress = string.Empty;
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(hostName);

                foreach (IPAddress ip in localIPs)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return ipAddress;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyname = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
