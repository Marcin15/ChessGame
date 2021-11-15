using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChessGame.Core
{
    public class Queen : Piece
    {
        private readonly Uri mImageUri;

        private readonly Uri BlackFigureImageSource = new(@"/Images/QueenBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new(@"/Images/QueenWhite.png", UriKind.Relative);

        public Queen(Player player, IField clickedField) : base(player, clickedField)
        {
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void GetAllowedMovesOfCurrentClickedPiece(IField clickedFigure, ObservableCollection<IField> fieldsList)
        { 
            List<Point> potentialMovesList = new();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    switch (i)
                    {
                        case 0:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex + j));
                            break;
                        case 1:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex - j));
                            break;
                        case 2:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex + j));
                            break;
                        case 3:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex - j));
                            break;
                        case 4:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex));
                            break;
                        case 5:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex));
                            break;
                        case 6:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex + j));
                            break;
                        case 7:
                            potentialMovesList.Add(new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex - j));
                            break;
                    }
                }
                base.GetAllowedMoves(potentialMovesList, fieldsList);

                potentialMovesList.Clear();
            }
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
