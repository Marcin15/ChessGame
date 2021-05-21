namespace ChessGame.Core
{
    public class RookFactory : IFigureFactory
    {
        private IField mField;
        private Player mPlayer;

        public RookFactory(IField field, Player player)
        {
            mField = field;
            mPlayer = player;
        }
        public Figure GetFigure()
        {
            return new Rook(mPlayer, mField);
        }
    }
}
