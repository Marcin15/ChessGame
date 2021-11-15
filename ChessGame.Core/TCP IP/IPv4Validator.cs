using System.Linq;

namespace ChessGame.Core
{
    public class IPv4Validator : IIPv4Validator
    {
        public bool Validate(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                return false;
            }

            var splitIpAddress = ipAddress.Split('.');

            return splitIpAddress.Length != 4 ? false : splitIpAddress.All(x => byte.TryParse(x, out byte tempForParsing));
        }
    }
}
