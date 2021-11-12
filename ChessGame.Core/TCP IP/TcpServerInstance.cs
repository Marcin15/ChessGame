using System.Net;
using System.Net.Sockets;

namespace ChessGame.Core
{
    public class TcpServerInstance
    {
        public static TcpListener TcpListener { get; set; }
        public static TcpListener StartServer()
        {
            var listener = new TcpListener(IPAddress.Any, 1302);
            listener.Start();

            return listener;
        }
    }
}
