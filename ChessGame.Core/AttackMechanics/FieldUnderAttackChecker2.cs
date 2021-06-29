using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class FieldUnderAttackChecker2
    {
        QueensAttackMechanics qam;
        public void Container(ObservableCollection<IField> fieldsList)
        {
            ResetUnderAttackFlag(fieldsList);
            ResetCheckFlag(fieldsList);

            PawnFieldUnderAttack(fieldsList);
            RookFieldUnderAttack(fieldsList);
            KnightFieldUnderAttack(fieldsList);
            BishopFieldUnderAttack(fieldsList);
            //QueenFieldUnderAttack(fieldsList);
            KingFieldUnderAttack(fieldsList);
            qam = new QueensAttackMechanics(fieldsList);
        }

        private void ResetCheckFlag(ObservableCollection<IField> fieldsList)
        {
            GameInfo.Check = false;

            foreach (var field in fieldsList.Where(x => x.IsUnderCheck).ToList())
                field.IsUnderCheck = false;
        }

        private void RaiseCheckFlag(List<IField> attackedFieldsWithCheck)
        {
            GameInfo.Check = true;

            foreach (var field in attackedFieldsWithCheck)
                field.IsUnderCheck = true;

        }

        private void ResetUnderAttackFlag(ObservableCollection<IField> fieldsList)
        {
            foreach (var field in fieldsList.Where(x => x.IsUnderAttack == true))
                field.IsUnderAttack = false;
        }

        private void PawnFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            var attackedFieldsWithCheck = new List<IField>();
            Point[] pointsAttackMoves = null;

            foreach (var pawn in fieldsList.Where(x => x.CurrentFigure is Pawn && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
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
                    attackedFieldsWithCheck.Add(pawn);

                    var attackField = fieldsList.Where(x => x.RowIndex == pointsAttackMoves[i].RowIndex && x.ColumnIndex == pointsAttackMoves[i].ColumnIndex)
                                                .FirstOrDefault();

                    if (attackField is not null)
                    {
                        attackedFieldsWithCheck.Add(attackField);
                        if (attackField.CurrentFigure is King &&
                           attackField.CurrentFigure.Player != GameInfo.CurrentPlayer)
                            RaiseCheckFlag(attackedFieldsWithCheck);

                        attackField.IsUnderAttack = true;
                        attackedFieldsWithCheck.Clear();
                    }
                }
            }
        }

        private void RookFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            var allowedMovesList = new List<Point>();
            var attackedFieldsWithCheck = new List<IField>();

            foreach (var rook in fieldsList.Where(x => x.CurrentFigure is Rook && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                allowedMovesList.Add(new Point(rook.RowIndex + j, rook.ColumnIndex));
                                break;
                            case 1:
                                allowedMovesList.Add(new Point(rook.RowIndex - j, rook.ColumnIndex));
                                break;
                            case 2:
                                allowedMovesList.Add(new Point(rook.RowIndex, rook.ColumnIndex + j));
                                break;
                            case 3:
                                allowedMovesList.Add(new Point(rook.RowIndex, rook.ColumnIndex - j));
                                break;
                        }
                    }
                    attackedFieldsWithCheck.Add(rook);
                    foreach (var point in allowedMovesList)
                    {
                        var attackField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                                  .FirstOrDefault();

                        if (attackField is not null)
                        {
                            attackedFieldsWithCheck.Add(attackField);
                            if (attackField.CurrentFigure is King &&
                                attackField.CurrentFigure.Player != GameInfo.CurrentPlayer)
                            {
                                RaiseCheckFlag(attackedFieldsWithCheck);
                            }

                            if (attackField.CurrentFigure == null ||
                                (attackField.CurrentFigure is King && attackField.CurrentFigure.Player != GameInfo.CurrentPlayer))

                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                          .Select(x => x.IsUnderAttack = true)
                                          .FirstOrDefault();
                            else
                            {
                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                            .Select(x => x.IsUnderAttack = true)
                                            .FirstOrDefault();

                                allowedMovesList.Clear();
                                break;
                            }
                        }
                    }
                    allowedMovesList.Clear();
                    attackedFieldsWithCheck.Clear();
                }
            }
        }

        private void KnightFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            var allowedMovesList = new List<Point>();
            var attackedFieldsWithCheck = new List<IField>();

            foreach (var knight in fieldsList.Where(x => x.CurrentFigure is Knight && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
                allowedMovesList = new List<Point>
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
                    attackedFieldsWithCheck.Add(knight);

                    var attackField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                              .FirstOrDefault();

                    if (attackField is not null)
                    {
                        attackedFieldsWithCheck.Add(attackField);

                        if (attackField.CurrentFigure is King &&
                            attackField.CurrentFigure.Player != GameInfo.CurrentPlayer)
                            RaiseCheckFlag(attackedFieldsWithCheck);

                        fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                        .Select(x => x.IsUnderAttack = true)
                                        .FirstOrDefault();

                        attackedFieldsWithCheck.Clear();
                    }
                }

            }
        }

        private void BishopFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            var allowedMovesList = new List<Point>();
            var attackedFieldsWithCheck = new List<IField>();

            foreach (var bishop in fieldsList.Where(x => x.CurrentFigure is Bishop && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                allowedMovesList.Add(new Point(bishop.RowIndex + j, bishop.ColumnIndex + j));
                                break;
                            case 1:
                                allowedMovesList.Add(new Point(bishop.RowIndex + j, bishop.ColumnIndex - j));
                                break;
                            case 2:
                                allowedMovesList.Add(new Point(bishop.RowIndex - j, bishop.ColumnIndex + j));
                                break;
                            case 3:
                                allowedMovesList.Add(new Point(bishop.RowIndex - j, bishop.ColumnIndex - j));
                                break;
                        }
                    }

                    attackedFieldsWithCheck.Add(bishop);

                    foreach (var point in allowedMovesList)
                    {
                        var attackField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                                  .FirstOrDefault();


                        if (attackField is not null)
                        {
                            attackedFieldsWithCheck.Add(attackField);

                            if (attackField.CurrentFigure is King &&
                                attackField.CurrentFigure.Player != GameInfo.CurrentPlayer)
                            {
                                RaiseCheckFlag(attackedFieldsWithCheck);
                            }

                            if (attackField.CurrentFigure == null ||
                                (attackField.CurrentFigure is King && attackField.CurrentFigure.Player != GameInfo.CurrentPlayer))

                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                          .Select(x => x.IsUnderAttack = true)
                                          .FirstOrDefault();
                            else
                            {
                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                            .Select(x => x.IsUnderAttack = true)
                                            .FirstOrDefault();

                                allowedMovesList.Clear();
                                break;
                            }
                        }
                    }
                    allowedMovesList.Clear();
                    attackedFieldsWithCheck.Clear();
                }
            }
        }

        private void QueenFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            var allowedMovesList = new List<Point>();
            var attackedFieldsWithCheck = new List<IField>();

            foreach (var queen in fieldsList.Where(x => x.CurrentFigure is Queen && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
                attackedFieldsWithCheck.Clear();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                allowedMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex + j));
                                break;
                            case 1:
                                allowedMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex - j));
                                break;
                            case 2:
                                allowedMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex + j));
                                break;
                            case 3:
                                allowedMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex - j));
                                break;
                            case 4:
                                allowedMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex));
                                break;
                            case 5:
                                allowedMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex));
                                break;
                            case 6:
                                allowedMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex + j));
                                break;
                            case 7:
                                allowedMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex - j));
                                break;
                        }
                    }

                    attackedFieldsWithCheck.Add(queen);

                    foreach (var point in allowedMovesList)
                    {
                        var attackField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                                  .FirstOrDefault();

                        if (attackField is not null)
                        {
                            attackedFieldsWithCheck.Add(attackField);
                            if (attackField.CurrentFigure is King &&
                                attackField.CurrentFigure.Player != GameInfo.CurrentPlayer &&
                                !GameInfo.Check)
                            {
                                RaiseCheckFlag(attackedFieldsWithCheck);
                            }

                            if (attackField.CurrentFigure == null ||
                                (attackField.CurrentFigure is King && attackField.CurrentFigure.Player != GameInfo.CurrentPlayer))

                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                          .Select(x => x.IsUnderAttack = true)
                                          .FirstOrDefault();
                            else
                            {
                                fieldsList.Where(x => x.RowIndex == attackField.RowIndex && x.ColumnIndex == attackField.ColumnIndex)
                                            .Select(x => x.IsUnderAttack = true)
                                            .FirstOrDefault();

                                allowedMovesList.Clear();
                                break;
                            }
                        }
                    }
                    allowedMovesList.Clear();
                    attackedFieldsWithCheck.Clear();
                }
            }
        }

        private void KingFieldUnderAttack(ObservableCollection<IField> fieldsList)
        {
            foreach (var king in fieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
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
