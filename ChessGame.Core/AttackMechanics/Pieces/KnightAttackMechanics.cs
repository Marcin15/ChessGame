﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class KnightAttackMechanics
    {
        private ObservableCollection<IField> mFieldsList;

        public KnightAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;

            GetQueensAttackMechanics();
        }
        public void GetQueensAttackMechanics()
        {
            var checkFinder = new CheckFinder(mFieldsList);

            List<Point> allowedMovesList = new();
            foreach (var knight in mFieldsList.Where(x => x.CurrentFigure is Knight && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {

                var potentialMovesList = new List<Point>
                {
                    new Point(knight.RowIndex, knight.ColumnIndex),
                    new Point(knight.RowIndex - 1, knight.ColumnIndex + 2), //1
                    new Point(knight.RowIndex + 1, knight.ColumnIndex + 2), //2
                    new Point(knight.RowIndex + 2, knight.ColumnIndex + 1), //3
                    new Point(knight.RowIndex + 2, knight.ColumnIndex - 1), //4
                    new Point(knight.RowIndex + 1, knight.ColumnIndex - 2), //5
                    new Point(knight.RowIndex - 1, knight.ColumnIndex - 2), //6
                    new Point(knight.RowIndex - 2, knight.ColumnIndex - 1), //7
                    new Point(knight.RowIndex - 2, knight.ColumnIndex + 1), //8
                };
                checkFinder.RaiseIsUnderAttackFlag(potentialMovesList, true);
            }
        }
    }
}