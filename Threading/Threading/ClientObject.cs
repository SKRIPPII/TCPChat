using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public class ClientObject
    {
        string Name;
        public string ID { get; } =  Guid.NewGuid().ToString();  
        ServerObject server;
        TcpClient client;
        public StreamReader reader;
        public StreamWriter writer;
        public ClientObject(TcpClient client,ServerObject serverObject) { this.client = client; reader = new(client.GetStream()); writer = new(client.GetStream()); server = serverObject; }

        internal void Process()
        {
            try
            {
                string name = reader.ReadLine();
                Name = name;
                server.BroadCastAsync(name + $" вошел в чат,поток : {Thread.CurrentThread.ManagedThreadId}", ID);
                Console.WriteLine(name + $" вошел в чат,поток : {Thread.CurrentThread.ManagedThreadId}", ID);
                while (true)
                {


                    try
                    {
                        string mes = reader.ReadLine();
                        if (!string.IsNullOrEmpty(mes))
                        {
                            Console.WriteLine($"{name}: {mes}");
                            server.BroadCastAsync($"{name}: {mes}", ID);
                        }

                    }
                    catch
                    {
                        string message = $"{name} покинул чат.";
                        server.BroadCastAsync(message, ID);
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
