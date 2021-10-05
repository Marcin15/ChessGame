using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public abstract class Piece
    {
        protected Uri defaultImageSource = new(@"/Images/Default.png", UriKind.Relative);
        public bool IsPinned { get; set; } = false;
        public Player Player { get; private set; }
        public int AllowedMovesCounter { get; protected set; } = 0;

        public Piece(Player player, IField clickedField)
        {
            this.Player = player;
            clickedField.CurrentFigure = this;
        }
        virtual public bool Move(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            clickedField.CurrentFigure = clickedFigure.CurrentFigure;
            clickedFigure.CurrentFigure = null;

            clickedField.IsClicked = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return false;
        }

        protected void GetAllowedMoves(List<Point> potentialMovesList, ObservableCollection<IField> fieldsList)
        {
            AllowedMovesCounter = 0;
            var checkCondition = true;

            foreach (var point in potentialMovesList)
            {
                var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                          .FirstOrDefault();

                if (moveField is not null)
                {
                    if (GameInfo.Check)
                        checkCondition = moveField.IsUnderCheck;
                    else if (this.IsPinned)
                        checkCondition = moveField.IsUnderPin;

                    if (checkCondition)
                    {
                        if (CheckIfMoveIsLegal(fieldsList, moveField))
                        {
                            potentialMovesList.Clear();
                            break;
                        }
                    }

                }
            }
        }

        protected void GetAllowedMoves(List<Point> potentialMovesList, ObservableCollection<IField> fieldsList, bool isKingOrKnight, bool isKing = false)
        {
            AllowedMovesCounter = 0;
            var checkCondition = true;
            var condition = false;

            foreach (var point in potentialMovesList)
            {
                var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                          .FirstOrDefault();

                condition = isKing ? (moveField is not null && !moveField.IsUnderAttack) : moveField is not null;

                if (condition)
                {
                    if (!isKing)
                    {
                        if (GameInfo.Check)
                            checkCondition = moveField.IsUnderCheck;
                        else if (this.IsPinned)
                            checkCondition = moveField.IsUnderPin;
                    }

                    if (checkCondition)
                        if (CheckIfMoveIsLegal(fieldsList, moveField))
                        {
                            continue;
                        }
                }
            }
        }

        private bool CheckIfMoveIsLegal(ObservableCollection<IField> fieldsList, IField moveField)
        {
            if (moveField.CurrentFigure == null)
            {
                fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                          .Select(x => x.FieldState = FieldState.MoveState)
                          .FirstOrDefault();

                AllowedMovesCounter++;
            }

            else
            {
                if (moveField.CurrentFigure.Player == this.Player)
                    return true;

                else
                {
                    fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                              .Select(x => x.FieldState = FieldState.CaptureState)
                              .FirstOrDefault();

                    AllowedMovesCounter++;
                    return true;
                }
            }

            return false;
        }

        public abstract void GetAllowedMovesOfCurrentClickedFigure(IField clickedFigure, ObservableCollection<IField> fieldsList);
    }
}
