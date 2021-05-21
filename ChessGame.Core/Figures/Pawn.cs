using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class Pawn : Figure
    {
        private bool mIsMoved = false;
        private Player mPlayer;
        private Uri mImageUri;
        private readonly Uri BlackFigureImageSource = new Uri(@"/Images/PawnBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new Uri(@"/Images/PawnWhite.png", UriKind.Relative);

        public Pawn(Player player, IField clickedField) : base(player, clickedField)
        {
            mPlayer = player;
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }
        public override ObservableCollection<IField> AllowedMoves(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            var figuresList = fieldsList.Where(x => x.CurrentFigure != null).ToList();
            Point[] pointsMoves = null;
            Point[] pointsAttackMoves = null;

            Point[] blackPawnMoves =
            {
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex)
            };

            Point[] blackPawnAttackMoves =
            {
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex - 1)
            };

            Point[] whitePawnMoves =
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex)
            };

            Point[] whitePawnAttackMoves =
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex - 1)
            };

            if (mPlayer == Player.Black)
            {
                pointsMoves = blackPawnMoves;
                pointsAttackMoves = blackPawnAttackMoves;
            }
            else if (mPlayer == Player.White)
            {
                pointsMoves = whitePawnMoves;
                pointsAttackMoves = whitePawnAttackMoves;
            }

            for (int i = 0; i < 2; i++)
            {
                if (fieldsList.Where(x => x.RowIndex == pointsMoves[i].RowIndex && x.ColumnIndex == pointsMoves[i].ColumnIndex).FirstOrDefault() != null)
                    fieldsList.Where(x => x.RowIndex == pointsMoves[i].RowIndex && x.ColumnIndex == pointsMoves[i].ColumnIndex).Select(x => x.FieldState = FieldState.MoveState).ToList();
                else
                    break;

                if (mIsMoved)
                    break;
            }

            //for (int i = 0; i < 2; i++)
            //{
            //    if (chessboardFigures.Where(x => x.Position.Equals(pointsAttack[i])).FirstOrDefault() != null)
            //        allowedMoves.Add(pointsAttack[i]);
            //}

            return fieldsList;
        }
        public override void Move(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            if (clickedField.FieldState == FieldState.MoveState)
            {
                base.Move(clickedFigure, clickedField, allowedMoves);
                clickedField.FigureImageSource = mImageUri;
                clickedFigure.FigureImageSource = defaultImageSource;

                if (!mIsMoved)
                    mIsMoved = true;
            }
        }
    }
}
