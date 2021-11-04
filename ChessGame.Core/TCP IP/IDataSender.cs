using System.Net.Sockets;

namespace ChessGame.Core
{
    public interface IDataSender
    {
        void SendData(TcpClient client, IField FromField, IField ToField);
    }
}