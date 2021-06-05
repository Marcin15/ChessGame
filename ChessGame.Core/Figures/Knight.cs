using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class Knight : Figure
    {
        private Player mPlayer;
        private Uri mImageUri;

        private readonly Uri BlackFigureImageSource = new Uri(@"/Images/KnightBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new Uri(@"/Images/KnightWhite.png", UriKind.Relative);
        public Knight(Player player, IField clickedField) : base(player, clickedField)
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
            var checkCondition = false;
            var allowedMovesList = new List<Point>
            {
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex + 2), //1
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex + 2), //2
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex + 1), //3
                new Point(clickedFigure.RowIndex + 2, clickedFigure.ColumnIndex - 1), //4
                new Point(clickedFigure.RowIndex + 1, clickedFigure.ColumnIndex - 2), //5
                new Point(clickedFigure.RowIndex - 1, clickedFigure.ColumnIndex - 2), //6
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex - 1), //7
                new Point(clickedFigure.RowIndex - 2, clickedFigure.ColumnIndex + 1), //8
            };

            foreach (var point in allowedMovesList)
            {
                var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                          .FirstOrDefault();


                if (GameInfo.Check)
                    checkCondition = moveField is not null && moveField.IsUnderCheck;
                else
                    checkCondition = true;

                if (checkCondition && moveField is not null)
                    GetAllowedMoves(clickedFigure, fieldsList, moveField);
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

                return true;
            }
            return false;
        }
    }
}
