using Newtonsoft.Json;

namespace ChessGame.Core
{
    public class Deserializer : IDeserializer
    {
        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
