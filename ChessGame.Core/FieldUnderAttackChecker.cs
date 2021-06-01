using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class FieldUnderAttackChecker
    {
        public void Container(ObservableCollection<IField> fieldsList)
        {
            ClearUnderAttackFlag(fieldsList);

            PawnFieldUnderAttack(fieldsList);
            RookFieldUnderAttack(fieldsList);
            KnightFieldUnderAttack(fieldsList);
            BishopFieldUnderAttack(fieldsList);
            QueenFieldUnderAttack(fieldsList);
            KingFieldUnderAttack(fieldsList);
        }

        private void ClearUnderAttackFlag(ObservableCollection<IField> fieldsList)
        {
            foreach (var field in fieldsList.Where(x => x.IsUnderAttack == true))
                field.IsUnderAttack = false;
        }

        private void PawnFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {

            foreach (var pawn in fieldsList.Where(x => x.CurrentFigure is Pawn && x.CurrentFigure.Player == GameStatus.CurrentPlayer).ToList())
            {
                Point[] pointsAttackMoves = null;

                Point[] whitePawnAttackMoves =
                {
                new Point(pawn.RowIndex - 1, pawn.ColumnIndex + 1),
                new Point(pawn.RowIndex - 1, pawn.ColumnIndex - 1)
                };

                Point[] blackPawnAttackMoves =
                {
                new Point(pawn.RowIndex + 1, pawn.ColumnIndex + 1),
                new Point(pawn.RowIndex + 1, pawn.ColumnIndex - 1)
                };

                if (pawn.CurrentFigure.Player == Player.Black)
                {
                    pointsAttackMoves = blackPawnAttackMoves;
                }
                else if (pawn.CurrentFigure.Player == Player.White)
                {
                    pointsAttackMoves = whitePawnAttackMoves;
                }

                for (int i = 0; i < 2; i++)
                {
                    var attackField = fieldsList.Where(x => x.RowIndex == pointsAttackMoves[i].RowIndex && x.ColumnIndex == pointsAttackMoves[i].ColumnIndex)
                                                .FirstOrDefault();

                    if (attackField is not null)
                        attackField.IsUnderAttack = true;
                }
            }
        }

        private void RookFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            foreach (var rook in fieldsList.Where(x => x.CurrentFigure is Rook && x.CurrentFigure.Player == GameStatus.CurrentPlayer).ToList())
            {
                List<Point> alloweMovesList = new List<Point>();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                alloweMovesList.Add(new Point(rook.RowIndex + j, rook.ColumnIndex));
                                break;
                            case 1:
                                alloweMovesList.Add(new Point(rook.RowIndex - j, rook.ColumnIndex));
                                break;
                            case 2:
                                alloweMovesList.Add(new Point(rook.RowIndex, rook.ColumnIndex + j));
                                break;
                            case 3:
                                alloweMovesList.Add(new Point(rook.RowIndex, rook.ColumnIndex - j));
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
                                          .Select(x => x.IsUnderAttack = true)
                                          .FirstOrDefault();
                            else
                            {
                                fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                            .Select(x => x.IsUnderAttack = true)
                                            .FirstOrDefault();

                                alloweMovesList.Clear();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void KnightFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            foreach (var knight in fieldsList.Where(x => x.CurrentFigure is Knight && x.CurrentFigure.Player == GameStatus.CurrentPlayer).ToList())
            {
                var allowedMovesList = new List<Point>
                {
                new Point(knight.RowIndex - 1, knight.ColumnIndex + 2), //1
                new Point(knight.RowIndex + 1, knight.ColumnIndex + 2), //2
                new Point(knight.RowIndex + 2, knight.ColumnIndex + 1), //3
                new Point(knight.RowIndex + 2, knight.ColumnIndex - 1), //4
                new Point(knight.RowIndex + 1, knight.ColumnIndex - 2), //5
                new Point(knight.RowIndex - 1, knight.ColumnIndex - 2), //6
                new Point(knight.RowIndex - 2, knight.ColumnIndex - 1), //7
                new Point(knight.RowIndex - 2, knight.ColumnIndex + 1), //8
                };

                foreach (var point in allowedMovesList)
                {
                    var attackField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                              .FirstOrDefault();

                    if (attackField is not null)
                    {
                        fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                        .Select(x => x.IsUnderAttack = true)
                                        .FirstOrDefault();
                    }
                }

            }
        }

        private void BishopFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            foreach (var bishop in fieldsList.Where(x => x.CurrentFigure is Bishop && x.CurrentFigure.Player == GameStatus.CurrentPlayer).ToList())
            {
                List<Point> alloweMovesList = new List<Point>();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                alloweMovesList.Add(new Point(bishop.RowIndex + j, bishop.ColumnIndex + j));
                                break;
                            case 1:
                                alloweMovesList.Add(new Point(bishop.RowIndex + j, bishop.ColumnIndex - j));
                                break;
                            case 2:
                                alloweMovesList.Add(new Point(bishop.RowIndex - j, bishop.ColumnIndex + j));
                                break;
                            case 3:
                                alloweMovesList.Add(new Point(bishop.RowIndex - j, bishop.ColumnIndex - j));
                                break;
                        }
                    }

                    foreach (var point in alloweMovesList)
                    {
                        var attackField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                                  .FirstOrDefault();

                        if (attackField is not null)
                        {
                            fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                        .Select(x => x.IsUnderAttack = true)
                                        .FirstOrDefault();

                            alloweMovesList.Clear();
                            break;
                        }
                    }
                }
            }
        }

        private void QueenFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            foreach (var queen in fieldsList.Where(x => x.CurrentFigure is Queen && x.CurrentFigure.Player == GameStatus.CurrentPlayer).ToList())
            {
                List<Point> alloweMovesList = new List<Point>();

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                alloweMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex + j));
                                break;
                            case 1:
                                alloweMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex - j));
                                break;
                            case 2:
                                alloweMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex + j));
                                break;
                            case 3:
                                alloweMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex - j));
                                break;
                            case 4:
                                alloweMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex));
                                break;
                            case 5:
                                alloweMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex));
                                break;
                            case 6:
                                alloweMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex + j));
                                break;
                            case 7:
                                alloweMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex - j));
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
                                          .Select(x => x.IsUnderAttack = true)
                                          .FirstOrDefault();
                            else
                            {
                                fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                            .Select(x => x.IsUnderAttack = true)
                                            .FirstOrDefault();

                                alloweMovesList.Clear();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void KingFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            foreach (var king in fieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player == GameStatus.CurrentPlayer).ToList())
            {
                var allowedMovesList = new List<Point>
            {
                new Point(king.RowIndex - 1, king.ColumnIndex + 1),
                new Point(king.RowIndex, king.ColumnIndex + 1),
                new Point(king.RowIndex + 1, king.ColumnIndex + 1),
                new Point(king.RowIndex + 1, king.ColumnIndex),
                new Point(king.RowIndex + 1, king.ColumnIndex - 1),
                new Point(king.RowIndex, king.ColumnIndex - 1),
                new Point(king.RowIndex - 1, king.ColumnIndex - 1),
                new Point(king.RowIndex - 1, king.ColumnIndex),
            };

                foreach (var point in allowedMovesList)
                {
                    var moveField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                              .FirstOrDefault();

                    if (moveField is not null)
                    {
                        fieldsList.Where(x => x.RowIndex == moveField.RowIndex && x.ColumnIndex == moveField.ColumnIndex)
                                        .Select(x => x.IsUnderAttack = true)
                                        .FirstOrDefault();
                    }
                }
            }
        }
    }
}
