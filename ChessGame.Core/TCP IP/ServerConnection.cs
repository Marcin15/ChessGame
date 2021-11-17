using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChessGame.Core
{
    public class ServerConnection : IServerConnection
    {
        public async Task<TcpClient> ConnectClientToServerAsync(CancellationToken token)
        {
            TcpClient client = null;
        connection:
            try
            {
                await Task.Run(() =>
                {
                    if(token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    client = new TcpClient(TcpClientInstance.ServerIp, 1302);

                }, token);

                return client is not null ? client : throw new Exception();
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                goto connection;
            }
        }
        
        public async Task<TcpClient> ConnectClientToServerAsync()
        {
            TcpClient client = null;
        connection:
            try
            {
                await Task.Run(() =>
                {
                    client = new TcpClient(TcpClientInstance.ServerIp, 1302);
                });

                return client is not null ? client : throw new Exception();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                goto connection;
            }
        }

        public async Task<TcpClient> ConnectClientToServerAsync(TcpListener listener, CancellationToken token)
        {
            TcpClient client = null;
        connection:
            try
            {
                await Task.Run(() =>
                {
                    if(token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    client = listener.AcceptTcpClient();

                }, token);

                return client is not null ? client : throw new Exception();
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                goto connection;
            }
        }
    }
}
