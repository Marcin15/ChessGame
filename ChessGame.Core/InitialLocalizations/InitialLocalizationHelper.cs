namespace ChessGame.Core
{
    public class InitialLocalizationHelper
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public string TypeOfFigure { get; private set; }
        public Player Player { get; private set; }
        public InitialLocalizationHelper(int row, int column, Player player, string typeOfFigure)
        {
            this.Row = row;
            this.Column = column;
            this.Player = player;
            this.TypeOfFigure = typeOfFigure;
        }
    }
}
