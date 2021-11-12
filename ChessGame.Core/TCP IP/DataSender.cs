using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ChessGame.Core
{
    public class DataSender : IDataSender
    {
        private readonly IServerConnection _ServerConnection;
        private readonly ISerializer _Serializer;
        private readonly MoveModel MoveObject = new();
        public DataSender(IServerConnection serverConnection,
                          ISerializer serializer)
        {
            _ServerConnection = serverConnection;
            _Serializer = serializer;
        }

        public void SendData(IField fromField, IField toField)
        {
            CreateMoveModel(fromField, toField);
            if (TcpClientInstance.TcpClient is null)
            {
                TcpClientInstance.TcpClient = _ServerConnection.ConnectClientToServer();
            }

            SendMessage(TcpClientInstance.TcpClient);
        }

        private void SendData()
        {
            TcpClientInstance.TcpClient = _ServerConnection.ConnectClientToServer();

            SendMessage(TcpClientInstance.TcpClient);
        }

        private void CreateMoveModel(IField fromField, IField toField)
        {
            MoveObject.From = new int[]
            {
                fromField.RowIndex,
                fromField.ColumnIndex
            };
            MoveObject.To = new int[]
            {
                toField.RowIndex,
                toField.ColumnIndex
            };

        }
        private void SendMessage(TcpClient client)
        {
            try
            {
                string serializedObject = _Serializer.Serialize(MoveObject);
                int byteCount = Encoding.UTF8.GetByteCount(serializedObject);
                byte[] sendData = new byte[byteCount];
                sendData = Encoding.UTF8.GetBytes(serializedObject);

                var networkStream = client.GetStream();
                networkStream.Write(sendData, 0, byteCount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                SendData();
            }
        }
    }
}
