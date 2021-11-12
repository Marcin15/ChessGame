namespace ChessGame.Core
{
    public interface IDeserializer
    {
        T Deserialize<T>(string json);
    }
}