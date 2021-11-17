using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChessGame.Core
{
    public interface IServerConnection
    {
        Task<TcpClient> ConnectClientToServerAsync(CancellationToken token);
        Task<TcpClient> ConnectClientToServerAsync(TcpListener listener, CancellationToken token);
        Task<TcpClient> ConnectClientToServerAsync();
    }
}