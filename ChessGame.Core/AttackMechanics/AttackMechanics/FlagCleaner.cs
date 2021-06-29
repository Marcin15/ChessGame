using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class FlagCleaner
    {
        ObservableCollection<IField> mFieldsList;
        public FlagCleaner(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;

            ClearIsPinnedFlag();
            ClearIsUnderPinFlag();
            ClearCheckFlag();
            ClearIsUnderAttackFlag();
        }

        private void ClearIsPinnedFlag()
        {
            foreach (var figures in mFieldsList.Where(x => x.CurrentFigure?.IsPinned == true).ToList())
            {
                figures.CurrentFigure.IsPinned = false;
            }
        }

        private void ClearIsUnderPinFlag()
        {
            for (int i = 0; i < mFieldsList.Count; i++)
            {
                IField fields = mFieldsList[i];
                if (fields.IsUnderPin)
                {
                    fields.IsUnderPin = false;
                }
            }
        }

        private void ClearCheckFlag()
        {
            GameInfo.Check = false;

            foreach (var field in mFieldsList.Where(x => x.IsUnderCheck).ToList())
                field.IsUnderCheck = false;
        }

        private void ClearIsUnderAttackFlag()
        {
            foreach (var field in mFieldsList.Where(x => x.IsUnderAttack == true))
                field.IsUnderAttack = false;
        }
    }
}
