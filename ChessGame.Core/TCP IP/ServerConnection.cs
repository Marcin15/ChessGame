using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace ChessGame.Core
{
    public class ServerConnection : IServerConnection
    {
        public TcpClient ConnectClientToServer()
        {
        connection:
            try
            {
                var client = new TcpClient(TcpClientInstance.ServerIp, 1302);

                return client is not null ? client : throw new Exception();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                goto connection;
            }
        }

        public TcpClient ConnectClientToServer(TcpListener listener)
        {
        connection:
            try
            {
                var client = listener.AcceptTcpClient();

                return client is not null ? client : throw new Exception();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                goto connection;
            }
        }
    }
}
