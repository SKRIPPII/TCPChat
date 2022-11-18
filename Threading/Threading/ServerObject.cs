using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public class ServerObject
    {
        TcpListener listener;
        List<ClientObject> clients;
        public ServerObject() { listener = new(IPAddress.Any, 8888);clients = new(); }

     public void Start()
        {
            listener.Start();
            Console.WriteLine("Сервер запущен,ожидание подключений...");
            try
            {
                while (true)
                {
                    var tcpclient = listener.AcceptTcpClient();
                    ClientObject client = new(tcpclient,this);
                    clients.Add(client);
                    Task.Run(() => client.Process());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            
        }
        public async Task BroadCastAsync(string message,string id)
        {
            foreach (var item in clients)
            {
                if(item.ID != id) { await item.writer.WriteLineAsync(message);await item.writer.FlushAsync(); }
            }
        }
    }
}
