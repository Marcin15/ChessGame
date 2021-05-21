namespace ChessGame.Core
{
    public class KnightFactory : IFigureFactory
    {
        private IField mField;
        private Player mPlayer;

        public KnightFactory(IField field, Player player)
        {
            mField = field;
            mPlayer = player;
        }
        public Figure GetFigure()
        {
            return new Knight(mPlayer, mField);
        }
    }
}
