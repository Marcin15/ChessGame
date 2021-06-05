using System;
using System.Collections.ObjectModel;
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
        public bool Passing { get; set; } = false;

        public Pawn(Player player, IField clickedField) : base(player, clickedField)
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
            Point[] forwardMoves = null;
            Point[] attackMoves = null;

            Point[] blackPawnForwardMoves =
            {
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex)
            };

            Point[] blackPawnAttackMoves =
            {
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex - 1)
            };

            Point[] whitePawnForwardMoves =
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex)
            };

            Point[] whitePawnAttackMoves =
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex - 1)
            };

            Point[] enPassantMoves =
            {
                new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex - 1)
            };

            if (mPlayer == Player.Black)
            {
                forwardMoves = blackPawnForwardMoves;
                attackMoves = blackPawnAttackMoves;
            }
            else if (mPlayer == Player.White)
            {
                forwardMoves = whitePawnForwardMoves;
                attackMoves = whitePawnAttackMoves;
            }

            AllowedMovesWhenItGoingForward(fieldsList, forwardMoves);
            AllowedMovesWhenItAttacking(clickedFigure, fieldsList, attackMoves);
        }

        private void AllowedMovesWhenItGoingForward(ObservableCollection<IField> fieldsList, Point[] forwardMoves)
        {
            var checkCondition = false;
            for (int i = 0; i < 2; i++)
            {
                var moveField = fieldsList.Where(x => x.RowIndex == forwardMoves[i].RowIndex && x.ColumnIndex == forwardMoves[i].ColumnIndex)
                                          .FirstOrDefault();

                if (GameInfo.Check)
                    checkCondition = moveField is not null && moveField.IsUnderCheck;
                else
                    checkCondition = true;

                if(checkCondition)
                {
                    if (moveField.CurrentFigure == null)
                    {
                        fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                  .Select(x => x.FieldState = FieldState.MoveState)
                                  .FirstOrDefault();
                    }
                    else
                        break;

                    if (mIsMoved)
                        break;
                }
            }
        }

        private void AllowedMovesWhenItAttacking(IField clickedFigure, ObservableCollection<IField> fieldsList, Point[] attackMoves)
        {
            var checkCondition = false;
            for (int i = 0; i < 2; i++)
            {
                var attackField = fieldsList.Where(x => x.RowIndex == attackMoves[i].RowIndex && x.ColumnIndex == attackMoves[i].ColumnIndex)
                                            .FirstOrDefault();

                if (GameInfo.Check)
                    checkCondition = attackField is not null && attackField.IsUnderCheck;
                else
                    checkCondition = true;

                if(checkCondition)
                {
                    if (attackField is not null)
                    {
                        if (attackField.CurrentFigure is not null)
                        {
                            if (attackField.CurrentFigure.Player != clickedFigure.CurrentFigure.Player)
                            {
                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                          .Select(x => x.FieldState = FieldState.CaptureState)
                                          .FirstOrDefault();
                            }
                        }
                    }
                }
            }
        }

        private void AllowedMovesWhenEnPassant(IField clickedFigure, ObservableCollection<IField> fieldsList, Point[] enPassantMoves)
        {
            //en passant (do poprawki)
            //for (int i = 0; i < 2; i++)
            //{
            //    var enPassantField = fieldsList.Where(x => x.RowIndex == enPassantMoves[i].RowIndex && x.ColumnIndex == enPassantMoves[i].ColumnIndex)
            //                                   .FirstOrDefault();

            //    if(enPassantField is not null)
            //    {
            //        if (enPassantField.CurrentFigure is Pawn enPassantPawn)
            //        {
            //            if (enPassantPawn.Passing)
            //            {
            //                fieldsList.Where(x => x.RowIndex == pointsAttackMoves[i].RowIndex && x.ColumnIndex == pointsAttackMoves[i].ColumnIndex)
            //                          .Select(x => x.FieldState = FieldState.MoveState)
            //                          .FirstOrDefault();
            //            }
            //            else
            //                break;
            //        }
            //    }
            //}
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
                {
                    mIsMoved = true;
                }

                //en passant
                if (clickedField.RowIndex == clickedFigure.RowIndex + 2 ||
                   clickedField.RowIndex == clickedFigure.RowIndex - 2)
                    Passing = true;

                return true;
            }
            return false;
        }
    }
}
