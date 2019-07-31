using System;
using System.Net.Sockets;

namespace WebBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer server = new WebServer();
            server.AddPrefix("http://localhost:8888/");
            server.Start();
            Console.ReadKey();
        }
    }
}

