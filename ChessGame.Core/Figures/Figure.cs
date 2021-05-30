using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
        public abstract void AllowedMoves(IField clickedFigure, ObservableCollection<IField> fieldsList);
    }
}
