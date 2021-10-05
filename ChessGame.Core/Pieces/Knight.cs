using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class Knight : Piece
    {
        private readonly Uri mImageUri;

        private readonly Uri BlackFigureImageSource = new(@"/Images/KnightBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new(@"/Images/KnightWhite.png", UriKind.Relative);
        public Knight(Player player, IField clickedField) : base(player, clickedField)
        {
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void GetAllowedMovesOfCurrentClickedFigure(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            var potentialMovesList = new List<Point>
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex + 2), //1
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex + 2), //2
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex + 1), //3
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex - 1), //4
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex - 2), //5
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex - 2), //6
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex - 1), //7
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex + 1), //8
            };
            base.GetAllowedMoves(potentialMovesList, fieldsList, true);
        }
        public override bool Move(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            if (clickedField.FieldState == FieldState.MoveState ||
                clickedField.FieldState == FieldState.CaptureState)
            {
                base.Move(clickedFigure, clickedField, allowedMoves);
                clickedField.FigureImageSource = mImageUri;
                clickedFigure.FigureImageSource = defaultImageSource;

                return true;
            }
            return false;
        }
    }
}
