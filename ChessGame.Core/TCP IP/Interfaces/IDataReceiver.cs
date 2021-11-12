using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChessGame.Core
{
    public interface IDataReceiver
    {
        Task ReadMessageAsync(TcpClient tcpClient, ObservableCollection<IField> fieldsList);
    }
}