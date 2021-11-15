using System.Linq;
using System.Net;

namespace ChessGame.Core
{
    public class ServerIpGetter : IServerIpGetter
    {
        public string ServerIp { get; private set; } = GetServerIp();
        private static string GetServerIp()
        {
            var hostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(hostName);

            return ipEntry.AddressList.Select(x => x.ToString()).FirstOrDefault(x => x.Length <= 15);
        }
    }
}
