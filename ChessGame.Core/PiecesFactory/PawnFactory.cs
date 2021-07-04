namespace ChessGame.Core
{
    public class PawnFactory : IFigureFactory
    {
        private IField mField;
        private Player mPlayer;

        public PawnFactory(IField field, Player player)
        {
            mField = field;
            mPlayer = player;
        }
        public Piece GetFigure()
        {
            return new Pawn(mPlayer, mField);
        }
    }
}
