using System.Net.Sockets;

namespace ChessGame.Core
{
    public interface IServerConnection
    {
        TcpClient ConnectClientToServer();
        TcpClient ConnectClientToServer(TcpListener listener);
    }
}