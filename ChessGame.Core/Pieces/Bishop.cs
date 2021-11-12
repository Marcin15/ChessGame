using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public class Bishop : Piece
    {
        private readonly Uri mImageUri;

        private readonly Uri BlackFigureImageSource = new(@"/Images/BishopBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new(@"/Images/BishopWhite.png", UriKind.Relative);
        public Bishop(Player player, IField clickedField) : base(player, clickedField)
        {
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void GetAllowedMovesOfCurrentClickedFigure(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            List<Point> potentialMovesList = new();

            for (int i = 0; i < 4; i++)
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
