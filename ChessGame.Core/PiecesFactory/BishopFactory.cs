namespace ChessGame.Core
{
    public class BishopFactory : IFigureFactory
    {
        private IField mField;
        private Player mPlayer;

        public BishopFactory(IField field, Player player)
        {
            mField = field;
            mPlayer = player;
        }
        public Piece GetFigure()
        {
            return new Bishop(mPlayer, mField);
        }
    }
}
