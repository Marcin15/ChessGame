using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class FieldUnderPinChecker
    {
        public FieldUnderPinChecker(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;
        }
        ObservableCollection<IField> mFieldsList;

        private bool mFirstMetFigureFlag = false;
        private IField mPotentialPinedFigure = null;

        public void PinFigure(List<Point> allowedMovesList)
        {
            var potentialPinedFieldsList = new List<IField>();
            bool firstElement = true;

            foreach (var point in allowedMovesList)
            {
                var attackedField = mFieldsList.FirstOrDefault(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex);

                if (attackedField is not null)
                {
                    if (mFirstMetFigureFlag &&
                       attackedField.CurrentFigure is not null &&
                       attackedField.CurrentFigure is not King)
                    {
                        potentialPinedFieldsList.Clear();
                        mPotentialPinedFigure = null;
                        mFirstMetFigureFlag = false;
                        break;
                    }

                    if (!mFirstMetFigureFlag &&
                        attackedField.CurrentFigure is not null &&
                        attackedField.CurrentFigure is not King &&
                        !firstElement)
                    {
                        mPotentialPinedFigure = attackedField;
                        mFirstMetFigureFlag = true;
                    }

                    if ((attackedField.CurrentFigure is King && attackedField.CurrentFigure.Player != GameInfo.CurrentPlayer) &&
                        mFirstMetFigureFlag)
                    {
                        mPotentialPinedFigure.CurrentFigure.IsPinned = true;
                        SetPinnedFields(potentialPinedFieldsList);
                        break;
                    }

                    potentialPinedFieldsList.Add(attackedField);
                    firstElement = false;
                }
            }
        }

        private void SetPinnedFields(List<IField> potentialPinedFieldsList)
        {
            foreach (var field in potentialPinedFieldsList)
            {
                field.IsUnderPin = true;
            }
        }
    }
}
