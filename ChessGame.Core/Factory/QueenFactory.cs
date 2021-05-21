namespace ChessGame.Core
{
    public class QueenFactory : IFigureFactory
    {
        private IField mField;
        private Player mPlayer;

        public QueenFactory(IField field, Player player)
        {
            mField = field;
            mPlayer = player;
        }
        public Figure GetFigure()
        {
            return new Queen(mPlayer, mField);
        }
    }
}
