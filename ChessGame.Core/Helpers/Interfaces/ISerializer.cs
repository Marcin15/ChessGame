namespace ChessGame.Core
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
    }
}