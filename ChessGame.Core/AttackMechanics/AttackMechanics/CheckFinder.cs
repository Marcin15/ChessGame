using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class CheckFinder
    {
        private ObservableCollection<IField> mFieldsList;
        readonly List<IField> mAttackedFieldsWithCheck = new();

        public CheckFinder(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;
        }

        public void RaiseIsUnderAttackFlag(List<Point> potentialMovesList)
        {
            bool firstElement = true;
            foreach (var point in potentialMovesList)
            {
                var attackedField = mFieldsList.FirstOrDefault(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex);

                if (firstElement)
                {
                    firstElement = false;
                    CheckSeeker(attackedField);
                    continue;
                }

                if (attackedField is not null)
                {
                    CheckSeeker(attackedField);

                    if (attackedField.CurrentFigure == null ||
                        (attackedField.CurrentFigure is King && attackedField.CurrentFigure.Player != GameInfo.CurrentPlayer))
                    {
                        attackedField.IsUnderAttack = true;
                    }

                    else
                    {
                        attackedField.IsUnderAttack = true;

                        mAttackedFieldsWithCheck.Clear();
                        break;
                    }
                }
            }
            mAttackedFieldsWithCheck.Clear();
        }

        public void RaiseIsUnderAttackFlag(List<Point> potentialMovesList, bool isKnight)
        {
            bool firstelement = true;
            foreach (var point in potentialMovesList)
            {
                var attackedField = mFieldsList.FirstOrDefault(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex);

                if (firstelement)
                {
                    firstelement = false;
                    CheckSeeker(attackedField);
                    continue;
                }


                if (attackedField is not null)
                {
                    CheckSeeker(attackedField);
                    attackedField.IsUnderAttack = true;
                }
            }
            mAttackedFieldsWithCheck.Clear();
        }

        private void CheckSeeker(IField attackedField)
        {
            mAttackedFieldsWithCheck.Add(attackedField);
            if (attackedField.CurrentFigure is King &&
                attackedField.CurrentFigure.Player != GameInfo.CurrentPlayer &&
                !GameInfo.Check)
            {
                RaiseCheckFlag(mAttackedFieldsWithCheck);
            }
        }

        private void RaiseCheckFlag(List<IField> attackedFieldsWithCheck)
        {
            GameInfo.Check = true;

            foreach (var field in attackedFieldsWithCheck)
                field.IsUnderCheck = true;
        }
    }
}
