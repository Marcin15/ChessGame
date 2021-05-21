namespace ChessGame.Core
{
    public class KingFactory : IFigureFactory
    {
        private IField mField;
        private Player mPlayer;

        public KingFactory(IField field, Player player)
        {
            mField = field;
            mPlayer = player;
        }
        public Figure GetFigure()
        {
            return new King(mPlayer, mField);
        }

    }
}
