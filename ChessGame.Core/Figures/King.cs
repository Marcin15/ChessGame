using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class King : Figure
    {
        private Player mPlayer;
        private Uri mImageUri;
        private bool mIsMoved = false;

        private readonly Uri BlackFigureImageSource = new Uri(@"/Images/KingBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new Uri(@"/Images/KingWhite.png", UriKind.Relative);
        public King(Player player, IField clickedField) : base(player, clickedField)
        {
            mPlayer = player;
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void AllowedMoves(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            var allowedMovesList = new List<Point>
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex - 1),
                new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex - 1),
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex - 1),
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex),
            };

            foreach (var point in allowedMovesList)
            {
                var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                          .FirstOrDefault();

                if (moveField is not null && !moveField.IsUnderAttack)
                {
                    if (moveField.CurrentFigure == null)
                    {
                        fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                      .Select(x => x.FieldState = FieldState.MoveState)
                                      .FirstOrDefault();
                    }
                    else
                    {
                        if (moveField.CurrentFigure.Player == clickedFigure.CurrentFigure.Player)
                            continue;
                        else
                        {
                            fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                  .Select(x => x.FieldState = FieldState.CaptureState)
                                  .FirstOrDefault();
                        }
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

                if (!mIsMoved)
                    mIsMoved = true;

                return true;
            }
            return false;
        }
    }
}
