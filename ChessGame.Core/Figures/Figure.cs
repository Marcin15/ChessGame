using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChessGame.Core
{
    public abstract class Figure
    {
        protected Uri defaultImageSource = new Uri(@"/Images/Default.png", UriKind.Relative);

        private Player mPlayer;
        public Figure(Player player, IField clickedField)
        {
            mPlayer = player;
            clickedField.CurrentFigure = this;
        }
        virtual public void Move(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            clickedField.CurrentFigure = clickedFigure.CurrentFigure;
            clickedFigure.CurrentFigure = null;

            clickedField.IsClicked = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public abstract ObservableCollection<IField> AllowedMoves(IField clickedFigure, ObservableCollection<IField> chessboardFields);
    }
}
