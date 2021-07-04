using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class CheckFinder : ICheckFinder
    {
        readonly List<IField> mAttackedFieldsWithCheck = new();

        public void FindFieldsUnderAttackAndCheck(List<Point> potentialMovesList, ObservableCollection<IField> fieldsList, bool isLinearAttackingPiece)
        {
            if (isLinearAttackingPiece)
                RaiseIsUnderAttackLinearAttackingPiece(potentialMovesList, fieldsList);
            else
                RaiseIsUnderAttackNonLinearAttackingPiece(potentialMovesList, fieldsList);
        }

        private void RaiseIsUnderAttackLinearAttackingPiece(List<Point> potentialMovesList, ObservableCollection<IField> fieldsList)
        {
            bool firstElement = true;
            foreach (var point in potentialMovesList)
            {
                var attackedField = fieldsList.FirstOrDefault(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex);

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

        private void RaiseIsUnderAttackNonLinearAttackingPiece(List<Point> potentialMovesList, ObservableCollection<IField> fieldsList)
        {
            bool firstElementFlag = true;
            IField firstElement = null;
            foreach (var point in potentialMovesList)
            {
                var attackedField = fieldsList.FirstOrDefault(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex);

                if (firstElementFlag)
                {
                    firstElementFlag = false;
                    firstElement = attackedField;
                    continue;
                }


                if (attackedField is not null)
                {
                    attackedField.IsUnderAttack = true;
                    CheckSeeker(firstElement);
                    if (CheckSeeker(attackedField))
                        break;
                }
                mAttackedFieldsWithCheck.Clear();
            }
        }

        private bool CheckSeeker(IField attackedField)
        {
            mAttackedFieldsWithCheck.Add(attackedField);
            if (attackedField.CurrentFigure is King &&
                attackedField.CurrentFigure.Player != GameInfo.CurrentPlayer &&
                !GameInfo.Check)
            {
                RaiseCheckFlag(mAttackedFieldsWithCheck);
                return true;
            }
            return false;
        }

        private void RaiseCheckFlag(List<IField> attackedFieldsWithCheck)
        {
            GameInfo.Check = true;

            foreach (var field in attackedFieldsWithCheck)
                field.IsUnderCheck = true;
        }
    }
}
