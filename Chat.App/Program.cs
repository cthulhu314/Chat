using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Client.WCF;
using Chat.Server.WCF;

namespace Chat.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) return;

            var type = args[0];

            if (type == "server")
            {
                
                using (var server = new ChatServer(new Uri("http://localhost:8001/chat")))
                {
                    server.Start();
                    Console.WriteLine("Server started and running at:");
                    foreach (var endpoint in server.Endpoints)
                    {
                        Console.WriteLine(endpoint);
                    }
                    Console.WriteLine("Press <ENTER> to stop server");
                    Console.ReadKey();
                    Console.WriteLine("Server stopping");
                }
                Console.WriteLine("Server stopped");
            }

            if (type == "client")
            {
                if(args.Length < 2) return;
                using (var client = new ChatClient(args[1]))
                {
                    string input;
                    client.OnMessageRecieved += (_, msg) => Console.WriteLine(msg);
                    Console.WriteLine("Client connected and ready for work");
                    while ((input = Console.ReadLine()) != "quit")
                    {
                        client.Send(input);
                    }
                    Console.WriteLine("Disconnecting");
                }
                Console.WriteLine("Disconnected");
            }
        }
    }
}
