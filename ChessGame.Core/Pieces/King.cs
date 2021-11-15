using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class King : Piece
    {
        private readonly Player mPlayer;
        private  readonly Uri mImageUri;
        private bool mIsMoved = false;
        private bool mCanCastle = false;

        private readonly Uri BlackFigureImageSource = new(@"/Images/KingBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new(@"/Images/KingWhite.png", UriKind.Relative);
        public King(Player player, IField clickedField) : base(player, clickedField)
        {
            mPlayer = player;
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void GetAllowedMovesOfCurrentClickedPiece(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            var potentialMovesList = new List<Point>
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

            base.GetAllowedMoves(potentialMovesList, fieldsList, true, true);

            if (!mIsMoved)
            {
                ShortCastle(fieldsList);
                LongCastle(fieldsList);
            }
        }

        private void ShortCastle(ObservableCollection<IField> fieldsList)
        {
            var rowIndex = (mPlayer == Player.White) ? 7 : 0;
            var bishopField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 5);
            var knightField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 6);
            var rookField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 7);
            var rook = rookField.CurrentFigure as Rook;

            if (rook is not null)
            {
                if (!rook.IsMoved &&
                    bishopField.CurrentFigure == null &&
                    knightField.CurrentFigure == null &&
                    !rookField.IsUnderAttack &&
                    !knightField.IsUnderAttack &&
                    !bishopField.IsUnderAttack &&
                    !GameInfo.Check)
                {
                    fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 6)
                                .Select(x => x.FieldState = FieldState.MoveState)
                                .FirstOrDefault();

                    mCanCastle = true;
                }
            }
        }

        private void LongCastle(ObservableCollection<IField> fieldsList)
        {
            var rowIndex = (mPlayer == Player.White) ? 7 : 0;
            var queenField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 3);
            var bishopField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 2);
            var knightField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 1);
            var rookField = fieldsList.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 0);
            var rook = rookField.CurrentFigure as Rook;

            if (rook is not null)
            {
                if (!rook.IsMoved &&
                    queenField.CurrentFigure == null &&
                    bishopField.CurrentFigure == null &&
                    knightField.CurrentFigure == null &&
                    !rookField.IsUnderAttack &&
                    !knightField.IsUnderAttack &&
                    !bishopField.IsUnderAttack &&
                    !queenField.IsUnderAttack &&
                    !GameInfo.Check)
                {
                    fieldsList.Where(x => x.RowIndex == rowIndex && x.ColumnIndex == 2)
                                .Select(x => x.FieldState = FieldState.MoveState)
                                .FirstOrDefault();

                    mCanCastle = true;
                }
            }
        }

        private void ShortCastleMove(int rowIndex, ObservableCollection<IField> allowedMoves)
        {
            var rookField = allowedMoves.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 7);
            var bishopField = allowedMoves.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 5);

            var rook = rookField.CurrentFigure as Rook;
            rook.Move(rookField, bishopField, allowedMoves);
            bishopField.IsClicked = false;
        }

        private void LongCastleMove(int rowIndex, ObservableCollection<IField> allowedMoves)
        {
            var rookField = allowedMoves.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 0);
            var queenField = allowedMoves.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 3);

            var rook = rookField.CurrentFigure as Rook;
            rook.Move(rookField, queenField, allowedMoves);
            queenField.IsClicked = false;
        }

        private void MoveCastle(IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            var rowIndex = (mPlayer == Player.White) ? 7 : 0;

            if (clickedField == allowedMoves.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 6))
                ShortCastleMove(rowIndex, allowedMoves);
            else if (clickedField == allowedMoves.FirstOrDefault(x => x.RowIndex == rowIndex && x.ColumnIndex == 2))
                LongCastleMove(rowIndex, allowedMoves);
        }

        public override bool Move(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            if (clickedField.FieldState == FieldState.MoveState ||
                clickedField.FieldState == FieldState.CaptureState)
            {
                base.Move(clickedFigure, clickedField, allowedMoves);
                clickedField.FigureImageSource = mImageUri;
                clickedFigure.FigureImageSource = defaultImageSource;

                if (mCanCastle)
                    MoveCastle(clickedField, allowedMoves);

                if (!mIsMoved)
                {
                    mIsMoved = true;
                    mCanCastle = false;
                }

                return true;
            }
            return false;
        }
    }
}
