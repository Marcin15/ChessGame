using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public abstract class Figure
    {
        protected Uri defaultImageSource = new Uri(@"/Images/Default.png", UriKind.Relative);

        public Player Player { get; private set; }
        public Figure(Player player, IField clickedField)
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
        protected bool GetAllowedMoves(IField clickedFigure, ObservableCollection<IField> fieldsList, IField moveField)
        {
            if (moveField.CurrentFigure == null)
                fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                          .Select(x => x.FieldState = FieldState.MoveState)
                          .FirstOrDefault();
            else
            {
                if (moveField.CurrentFigure.Player == clickedFigure.CurrentFigure.Player)
                    return true;

                else
                {
                    fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                              .Select(x => x.FieldState = FieldState.CaptureState)
                              .FirstOrDefault();

                    return true;
                }
            }

            return false;
        }

        public abstract void GetAllowedMovesOfCurrentClickedFigure(IField clickedFigure, ObservableCollection<IField> fieldsList);
    }
}
