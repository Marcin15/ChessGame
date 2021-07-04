using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class KingsAttacksMechanics : IKingsAttacksMechanics
    {
        private readonly ICheckFinder mCheckFinder;
        public KingsAttacksMechanics(ICheckFinder checkFinder)
        {
            mCheckFinder = checkFinder;
        }
        public void GetKingsAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            List<Point> allowedMovesList = new();
            foreach (var king in fieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
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
                mCheckFinder.FindFieldsUnderAttackAndCheck(potentialMovesList, fieldsList, false);
            }
        }
    }
}
