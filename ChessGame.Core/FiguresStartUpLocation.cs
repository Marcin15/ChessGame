namespace ChessGame.Core
{
    public class FiguresStartUpLocation
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public string TypeOfFigure { get; set; }
        public Player Player { get; private set; }
        public FiguresStartUpLocation(int row, int column, Player player, string typeOfFigure)
        {
            this.Row = row;
            this.Column = column;
            this.Player = player;
            this.TypeOfFigure = typeOfFigure;
        }
    }
}
