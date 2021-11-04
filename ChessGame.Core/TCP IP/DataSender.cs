using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ChessGame.Core
{
    public class DataSender : IDataSender
    {
        private readonly IServerConnection _ServerConnection;
        private MoveModel MoveObject = new();
        public DataSender(IServerConnection serverConnection)
        {
            _ServerConnection = serverConnection;
        }

        public void SendData(TcpClient client, IField fromField, IField toField)
        {
            CreateMoveModel(fromField, toField);
            if (client is null)
            {
                client = _ServerConnection.ConnectClientToServer();
            }

            SendMessage(client);
        }

        private void SendData()
        {
            var client = _ServerConnection.ConnectClientToServer();

            SendMessage(client);
        }

        private void CreateMoveModel(IField fromField, IField toField)
        {
            //MoveObject = new MoveModel
            //{
            //    From = new int[]
            //    {
            //        fromField.RowIndex,
            //        fromField.ColumnIndex
            //    },
            //    To = new int[]
            //    {
            //        toField.RowIndex,
            //        toField.ColumnIndex
            //    }
            //};


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
                string serializedObject = Serializer.Serialize(MoveObject);
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
