using Newtonsoft.Json;

namespace ChessGame.Core
{
    public class Serializer : ISerializer
    {
        public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
    }
}
