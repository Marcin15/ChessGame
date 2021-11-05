using System.Net.Sockets;

namespace ChessGame.Core
{
    public interface IDataSender
    {
        void SendData(IField FromField, IField ToField);
    }
}