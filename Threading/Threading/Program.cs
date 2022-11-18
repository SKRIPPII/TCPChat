using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using System.Reflection;
using System.Runtime.Loader;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Net.Sockets;
using Flurl;
using Threading;

namespace Test  
{
    class Program
    {
        static async Task Main()
        {
            ServerObject server = new ServerObject();// создаем сервер
            server.Start();
        }
    }
}
