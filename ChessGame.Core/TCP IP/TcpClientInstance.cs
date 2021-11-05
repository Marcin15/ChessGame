using System.Net.Sockets;

namespace ChessGame.Core
{
    public static class TcpClientInstance
    {
        public static TcpClient TcpClient { get; set; }
        public static string ServerIp { get; set; }
    }
}
