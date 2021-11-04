using Newtonsoft.Json;

namespace ChessGame.Core
{
    class Serializer
    {
        public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
    }
}
