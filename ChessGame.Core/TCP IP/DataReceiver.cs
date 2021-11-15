using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Core
{
    public class DataReceiver : IDataReceiver
    {
        private readonly IDeserializer _Deserializer;
        private readonly IPieceInteractionManager _PieceInteractionManager;
        public DataReceiver(IDeserializer deserializer,
                            IPieceInteractionManager pieceInteractionManager)
        {
            _Deserializer = deserializer;
            _PieceInteractionManager = pieceInteractionManager;
        }
        public async Task ReadMessageAsync(TcpClient tcpClient, ObservableCollection<IField> fieldsList)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            do
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    await networkStream.ReadAsync(buffer, 0, buffer.Length);

                    int recv = buffer.Where(b => b != 0).Count();

                    var response = Encoding.UTF8.GetString(buffer, 0, recv);

                    var obj = _Deserializer.Deserialize<MoveModel>(response);

                    var previousClickedPiece = fieldsList.FirstOrDefault(x => x.RowIndex == obj.From[0] && x.ColumnIndex == obj.From[1]);
                    var clickedField = fieldsList.FirstOrDefault(x => x.RowIndex == obj.To[0] && x.ColumnIndex == obj.To[1]);

                    clickedField.FieldState = FieldState.MoveState;

                    _PieceInteractionManager.Invoke(previousClickedPiece, clickedField, fieldsList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            } while (true);
        }
    }
}
