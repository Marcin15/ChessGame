using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class Pawn : Piece
    {
        private bool mIsMoved = false;
        private Player mPlayer;
        private Uri mImageUri;

        private IField pawnToCaptureEnPassant;
        private bool isEnPassantPossible = false;

        private readonly Uri BlackFigureImageSource = new(@"/Images/PawnBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new(@"/Images/PawnWhite.png", UriKind.Relative);
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
            Point[] potencialEnPassantMoves =
            {
                new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex - 1)
            };

            AllowedMovesWhenItGoingForward(fieldsList, PotencialForwardMoves(clickedFigure));
            AllowedMovesWhenItAttacking(fieldsList, PotencialAttackMoves(clickedFigure));
            AllowedMovesWhenEnPassant(fieldsList, potencialEnPassantMoves, PotencialAttackMoves(clickedFigure));
        }

        private Point[] PotencialForwardMoves(IField clickedFigure)
        {
            Point[] blackPawnPotencialForwardMoves =
{
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex)
            };

            Point[] whitePawnPotencialForwardMoves =
{
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex),
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex)
            };

            return (mPlayer == Player.Black) ? blackPawnPotencialForwardMoves : whitePawnPotencialForwardMoves;
        }

        private Point[] PotencialAttackMoves(IField clickedFigure)
        {
            Point[] blackPawnPotencialAttackMoves =
            {
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex - 1)
            };

            Point[] whitePawnPotencialAttackMoves =
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex + 1),
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex - 1)
            };

            return (mPlayer == Player.Black) ? blackPawnPotencialAttackMoves : whitePawnPotencialAttackMoves;
        }

        private void AllowedMovesWhenItGoingForward(ObservableCollection<IField> fieldsList, Point[] potencialForwardMoves)
        {
            var checkCondition = true;
            for (int i = 0; i < 2; i++)
            {
                var moveField = fieldsList.Where(x => x.RowIndex == potencialForwardMoves[i].RowIndex && x.ColumnIndex == potencialForwardMoves[i].ColumnIndex)
                                          .FirstOrDefault();

                if (GameInfo.Check)
                    checkCondition = moveField is not null && moveField.IsUnderCheck;
                else if (this.IsPinned)
                    checkCondition = moveField.IsUnderPin;

                if (checkCondition)
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

        private void AllowedMovesWhenItAttacking(ObservableCollection<IField> fieldsList, Point[] potencialAttackMoves)
        {
            var checkCondition = true;
            for (int i = 0; i < 2; i++)
            {
                var attackField = fieldsList.Where(x => x.RowIndex == potencialAttackMoves[i].RowIndex && x.ColumnIndex == potencialAttackMoves[i].ColumnIndex)
                                            .FirstOrDefault();

                if (GameInfo.Check)
                    checkCondition = attackField is not null && attackField.IsUnderCheck;
                else if (this.IsPinned)
                    checkCondition = attackField.IsUnderPin;

                if (checkCondition)
                {
                    if (attackField is not null)
                    {
                        if (attackField.CurrentFigure is not null)
                        {
                            if (attackField.CurrentFigure.Player != mPlayer)
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

        private void AllowedMovesWhenEnPassant(ObservableCollection<IField> fieldsList, Point[] potencialEnPassantMoves, Point[] potencialAttackMoves)
        {
            for (int i = 0; i < 2; i++)
            {
                var enPassantField = fieldsList.Where(x => x.RowIndex == potencialEnPassantMoves[i].RowIndex && x.ColumnIndex == potencialEnPassantMoves[i].ColumnIndex)
                                               .FirstOrDefault();

                if (enPassantField is not null && 
                    enPassantField.CurrentFigure is Pawn enPassantPawn)
                {
                    if (enPassantPawn.Passing)
                    {
                        pawnToCaptureEnPassant = fieldsList.FirstOrDefault(x => x.RowIndex == potencialEnPassantMoves[i].RowIndex && x.ColumnIndex == potencialEnPassantMoves[i].ColumnIndex);

                        fieldsList.Where(x => x.RowIndex == potencialAttackMoves[i].RowIndex && x.ColumnIndex == potencialAttackMoves[i].ColumnIndex)
                        .Select(x => x.FieldState = FieldState.MoveState)
                        .FirstOrDefault();

                        isEnPassantPossible = true;
                    }
                }
            }
        }

        private void EnPassantMove(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            Point[] potencialAttackMoves = PotencialAttackMoves(clickedFigure);

            for (int i = 0; i < 2; i++)
            {
                if (clickedField == allowedMoves.FirstOrDefault(x => x.RowIndex == potencialAttackMoves[i].RowIndex && x.ColumnIndex == potencialAttackMoves[i].ColumnIndex))
                {
                    pawnToCaptureEnPassant.CurrentFigure = null;
                    pawnToCaptureEnPassant.FigureImageSource = defaultImageSource;
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
                {
                    mIsMoved = true;
                }

                if(isEnPassantPossible)
                {
                    EnPassantMove(clickedFigure, clickedField, allowedMoves);
                }

                //en passant
                if (clickedField.RowIndex == clickedFigure.RowIndex + 2 ||
                   clickedField.RowIndex == clickedFigure.RowIndex - 2)
                {
                    Passing = true;
                }

                pawnToCaptureEnPassant = null;
                isEnPassantPossible = false;

                return true;
            }
            return false;
        }
    }
}
