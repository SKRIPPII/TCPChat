using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Test
{
    class Program
    {
        static async Task Main()
        {
            TcpClient client = new();
            client.Connect("127.0.0.1",8888);
            StreamReader reader = new(client.GetStream());
            StreamWriter writer = new(client.GetStream());
            Task.Run(()=>ReceiveMessageAsync(reader));
            await SendMessageAsync(writer);
        }
      static async Task ReceiveMessageAsync(StreamReader reader)
        {
            while (true)
            {
                string message = reader.ReadLine();
                if (!string.IsNullOrEmpty(message)) Console.WriteLine(message);
            }

        }
        static async Task SendMessageAsync(StreamWriter writer)
        {
            Console.WriteLine("Введите свое имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Добро пожаловать в чат: {0}", name);
            await writer.WriteLineAsync(name);
            await writer.FlushAsync();
            while (true)
            {
                string message = Console.ReadLine();
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();  
            }

        }
    }
}