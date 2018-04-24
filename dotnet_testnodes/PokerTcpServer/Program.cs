using Poker.Shared.CardsLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            var _handsByPlayerMac = new Dictionary<string, Hand>();
            // Parse("192.168.1.118")
            var listener = new TcpListener(localaddr: IPAddress.Parse("192.168.1.118"), port:7676);
            var random = new Random();
            while (true)
            {
                listener.Start();
                Console.WriteLine("Awaiting client to connect...");
                var client = listener.AcceptTcpClient();
                client.NoDelay = true;

                // Determine mac
                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                var mac = GetMacAddress(remoteIpEndPoint.Address);
                var macString = string.Join(",", mac);

                // Only draw a new hand for the client if he didnt get one before
                if(false == _handsByPlayerMac.ContainsKey(macString))
                {
                    _handsByPlayerMac.Add(macString, deck.DrawHand());
                }

                // Use map to resolve
                var data = new byte[4];
                var serializer = new HandSerializer();
                int index = 0;
                var hand = _handsByPlayerMac[macString];
                serializer.Serialize(hand: hand, outputData: data, index: ref index);

                Console.WriteLine("Sending hand: " + hand + " to client " + client.Client.RemoteEndPoint + ", mac: " + macString);

                client.Client.Send(data);
                
                Console.WriteLine("Closing connection again...");
                client.Close();
            }
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(uint destIP, uint srcIP, byte[] macAddress, ref uint macAddressLength);

        public static byte[] GetMacAddress(IPAddress address)
        {
            byte[] mac = new byte[6];
            uint len = (uint)mac.Length;
            byte[] addressBytes = address.GetAddressBytes();
            uint dest = ((uint)addressBytes[3] << 24)
              + ((uint)addressBytes[2] << 16)
              + ((uint)addressBytes[1] << 8)
              + ((uint)addressBytes[0]);
            if (SendARP(dest, 0, mac, ref len) != 0)
            {
                throw new Exception("The ARP request failed.");
            }
            return mac;
        }
    }
}
