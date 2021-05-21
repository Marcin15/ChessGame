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
        public Figure GetFigure()
        {
            return new Bishop(mPlayer, mField);
        }
    }
}
