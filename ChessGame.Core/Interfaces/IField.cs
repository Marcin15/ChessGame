using System;

namespace ChessGame.Core
{
    public interface IField
    {
        FieldState FieldState { get; set; }
        bool IsClicked { get; set; }
        int RowIndex { get; set; }
        int ColumnIndex { get; set; }
        public Uri FigureImageSource { get; set; }
        public Figure CurrentFigure { get; set; }
    }
}
