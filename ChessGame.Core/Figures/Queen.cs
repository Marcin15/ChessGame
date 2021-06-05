using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class Queen : Figure
    {
        private Player mPlayer;
        private Uri mImageUri;

        private readonly Uri BlackFigureImageSource = new Uri(@"/Images/QueenBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new Uri(@"/Images/QueenWhite.png", UriKind.Relative);

        public Queen(Player player, IField clickedField) : base(player, clickedField)
        {
            mPlayer = player;
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void GetAllowedMovesOfCurrentClickedFigure(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            var checkCondition = false;
            List<Point> alloweMovesList = new List<Point>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    switch (i)
                    {
                        case 0:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex + j));
                            break;
                        case 1:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex - j));
                            break;
                        case 2:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex + j));
                            break;
                        case 3:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex - j));
                            break;
                        case 4:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex));
                            break;
                        case 5:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex));
                            break;
                        case 6:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex + j));
                            break;
                        case 7:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex - j));
                            break;
                    }
                }
                foreach (var point in alloweMovesList)
                {
                    var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                              .FirstOrDefault();

                    if (GameInfo.Check)
                        checkCondition = moveField is not null && moveField.IsUnderCheck;
                    else
                        checkCondition = true;

                    if (checkCondition && moveField is not null)
                        if (GetAllowedMoves(clickedFigure, fieldsList, moveField))
                        {
                            alloweMovesList.Clear();
                            break;
                        }
                }
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
