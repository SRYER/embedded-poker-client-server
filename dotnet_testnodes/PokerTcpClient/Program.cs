using Poker.Shared.CardsLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            while (true)
            {
                DoRoundtrip();
                Console.WriteLine("Press any key to go again...");
                Console.ReadLine();
            }
        }

        private static void DoRoundtrip()
        {
            var client = new TcpClient();
            Console.WriteLine("Connecting...");
            client.Connect("localhost", 7676);
            Console.WriteLine("Connected. awaiting hand...");
            using (var r = new BinaryReader(client.GetStream()))
            {
                var handBytes = r.ReadBytes(count:4);
                var serializer = new HandSerializer();
                var index = 0;
                var hand = serializer.DeserializeHand(inputData: handBytes, index: ref index);
                Console.WriteLine("Received hand: " + hand);
            }
            Console.WriteLine("Done");
        }
    }
}
