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
        private bool mCanCastle = false;

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
                if (!mIsMoved)
                {
                    ShortCastle(fieldsList);
                    LongCastle(fieldsList);
                }
            }
        }

        private void ShortCastle(ObservableCollection<IField> fieldsList)
        {
            var rowIndex = (mPlayer == Player.White) ? 7 : 0;
            var bishopField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 5).FirstOrDefault();
            var knightField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 6).FirstOrDefault();
            var rookField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 7).FirstOrDefault();
            var rook = rookField.CurrentFigure as Rook;
            if (!rook.IsMoved &&
                bishopField.CurrentFigure == null &&
                knightField.CurrentFigure == null)
            {
                fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 6)
                            .Select(x => x.FieldState = FieldState.MoveState)
                            .FirstOrDefault();

                mCanCastle = true;
            }
        }

        private void LongCastle(ObservableCollection<IField> fieldsList)
        {
            var rowIndex = (mPlayer == Player.White) ? 7 : 0;
            var queenField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 3).FirstOrDefault();
            var bishopField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 2).FirstOrDefault();
            var knightField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 1).FirstOrDefault();
            var rookField = fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 0).FirstOrDefault();
            var rook = rookField.CurrentFigure as Rook;
            if (!rook.IsMoved &&
                queenField.CurrentFigure == null &&
                bishopField.CurrentFigure == null &&
                knightField.CurrentFigure == null)
            {
                fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 2)
                            .Select(x => x.FieldState = FieldState.MoveState)
                            .FirstOrDefault();

                mCanCastle = true;
            }
        }

        private void MoveCastle(IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            var rowIndex = (mPlayer == Player.White) ? 7 : 0;

            if (clickedField == allowedMoves.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 6).FirstOrDefault())
                ShortCastleMove(rowIndex, allowedMoves);
            else if (clickedField == allowedMoves.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 2).FirstOrDefault())
                LongCastleMove(rowIndex, allowedMoves);
        }

        private void ShortCastleMove(int rowIndex, ObservableCollection<IField> allowedMoves)
        {
            var rookField = allowedMoves.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 7).FirstOrDefault();
            var newRookField = allowedMoves.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 5).FirstOrDefault();

            var rook = rookField.CurrentFigure as Rook;
            rook.Move(rookField, newRookField, allowedMoves);
            newRookField.IsClicked = false;
        }
        private void LongCastleMove(int rowIndex, ObservableCollection<IField> allowedMoves)
        {
            var rookField = allowedMoves.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 0).FirstOrDefault();
            var newRookField = allowedMoves.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 3).FirstOrDefault();

            var rook = rookField.CurrentFigure as Rook;
            rook.Move(rookField, newRookField, allowedMoves);
            newRookField.IsClicked = false;
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

                if (mCanCastle)
                    MoveCastle(clickedField, allowedMoves);

                return true;
            }
            return false;
        }
    }
}
