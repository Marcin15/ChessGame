using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

                if (client is not null)
                {
                    return client;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                goto connection;
            }
        }
    }
}
