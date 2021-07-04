using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class FlagCleaner : IFlagCleaner
    {
        public void Clean(ObservableCollection<IField> fieldsList)
        {
            ClearIsPinnedFlag(fieldsList);
            ClearIsUnderPinFlag(fieldsList);
            ClearCheckFlag(fieldsList);
            ClearIsUnderAttackFlag(fieldsList);
        }

        private void ClearIsPinnedFlag(ObservableCollection<IField> fieldsList)
        {
            foreach (var figures in fieldsList.Where(x => x.CurrentFigure?.IsPinned == true).ToList())
            {
                figures.CurrentFigure.IsPinned = false;
            }
        }

        private void ClearIsUnderPinFlag(ObservableCollection<IField> fieldsList)
        {
            for (int i = 0; i < fieldsList.Count; i++)
            {
                IField fields = fieldsList[i];
                if (fields.IsUnderPin)
                {
                    fields.IsUnderPin = false;
                }
            }
        }

        private void ClearCheckFlag(ObservableCollection<IField> fieldsList)
        {
            GameInfo.Check = false;

            foreach (var field in fieldsList.Where(x => x.IsUnderCheck).ToList())
                field.IsUnderCheck = false;
        }

        private void ClearIsUnderAttackFlag(ObservableCollection<IField> fieldsList)
        {
            foreach (var field in fieldsList.Where(x => x.IsUnderAttack == true))
                field.IsUnderAttack = false;
        }
    }
}
