using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChessGame.Core
{
    public class Queen : Figure
    {
        private Player mPlayer;
        private Uri mImageUri;

        private readonly Uri BlackFigureImageSource = new Uri(@"/Images/QueenBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new Uri(@"/Images/QueenWhite.png", UriKind.Relative);

        public Queen(Player player, IField clickedField) : base(player, clickedField)
        {
            mPlayer = player;
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override ObservableCollection<IField> AllowedMoves(IField clickedFigure, ObservableCollection<IField> chessboardFields)
        {
            return null;
        }
        public override void Move(IField clickedFigure, IField clickedField, ObservableCollection<IField> allowedMoves)
        {
            base.Move(clickedFigure, clickedField, allowedMoves);
            clickedField.FigureImageSource = mImageUri;
            clickedFigure.FigureImageSource = defaultImageSource;
        }
    }
}
