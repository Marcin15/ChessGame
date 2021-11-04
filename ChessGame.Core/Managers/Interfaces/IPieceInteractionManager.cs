using System.Collections.ObjectModel;
using System.Net.Sockets;

namespace ChessGame.Core
{
    public interface IPieceInteractionManager
    {
        void Container(IField clickedField, ObservableCollection<IField> fieldsList, TcpClient tcpClient);
    }
}