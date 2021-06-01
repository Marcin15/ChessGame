using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class Rook : Figure
    {
        private Player mPlayer;
        private Uri mImageUri;
        public bool IsMoved { get; set; } = false;

        private readonly Uri BlackFigureImageSource = new Uri(@"/Images/RookBlack.png", UriKind.Relative);
        private readonly Uri WhiteFigureImageSource = new Uri(@"/Images/RookWhite.png", UriKind.Relative);

        public Rook(Player player, IField clickedField) : base(player, clickedField)
        {
            mPlayer = player;
            if (player == Player.Black)
                mImageUri = BlackFigureImageSource;
            else if (player == Player.White)
                mImageUri = WhiteFigureImageSource;

            clickedField.FigureImageSource = mImageUri;
        }

        public override void AllowedMoves(IField clickedFigure, ObservableCollection<IField> fieldsList)
        {
            List<Point> alloweMovesList = new List<Point>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    switch (i)
                    {
                        case 0:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex + j, clickedFigure.ColumnIndex));
                            break;
                        case 1:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex - j, clickedFigure.ColumnIndex));
                            break;
                        case 2:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex + j));
                            break;
                        case 3:
                            alloweMovesList.Add(new Point(clickedFigure.RowIndex, clickedFigure.ColumnIndex - j));
                            break;
                    }
                }
                foreach (var point in alloweMovesList)
                {
                    var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                              .FirstOrDefault();

                    if (moveField is not null)
                    {
                        if (moveField.CurrentFigure == null)
                            fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                      .Select(x => x.FieldState = FieldState.MoveState)
                                      .FirstOrDefault();
                        else
                        {
                            if (moveField.CurrentFigure.Player == clickedFigure.CurrentFigure.Player)
                            {
                                alloweMovesList.Clear();
                                break;
                            }
                            else
                            {
                                fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                          .Select(x => x.FieldState = FieldState.CaptureState)
                                          .FirstOrDefault();

                                alloweMovesList.Clear();
                                break;
                            }

                        }
                    }
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

                if (!this.IsMoved)
                    this.IsMoved = true;

                return true;
            }
            return false;
        }
    }
}
