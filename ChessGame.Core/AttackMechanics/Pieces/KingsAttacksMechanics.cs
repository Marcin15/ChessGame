using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class KingsAttacksMechanics
    {
        private ObservableCollection<IField> mFieldsList;

        public KingsAttacksMechanics(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;

            GetQueensAttackMechanics();
        }
        public void GetQueensAttackMechanics()
        {
            var checkFinder = new CheckFinder(mFieldsList);

            List<Point> allowedMovesList = new();
            foreach (var king in mFieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {

                var potentialMovesList = new List<Point>
                {
                new Point(king.RowIndex, king.ColumnIndex),
                new Point(king.RowIndex - 1, king.ColumnIndex + 1),
                new Point(king.RowIndex, king.ColumnIndex + 1),
                new Point(king.RowIndex + 1, king.ColumnIndex + 1),
                new Point(king.RowIndex + 1, king.ColumnIndex),
                new Point(king.RowIndex + 1, king.ColumnIndex - 1),
                new Point(king.RowIndex, king.ColumnIndex - 1),
                new Point(king.RowIndex - 1, king.ColumnIndex - 1),
                new Point(king.RowIndex - 1, king.ColumnIndex),
                };
                checkFinder.RaiseIsUnderAttackFlag(potentialMovesList, true);
            }
        }
    }
}
