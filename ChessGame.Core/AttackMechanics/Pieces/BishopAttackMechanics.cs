using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class BishopAttackMechanics : IBishopAttackMechanics
    {
        private readonly IFieldUnderPinChecker mFieldUnderPinChecker;
        private readonly ICheckFinder mCheckFinder;

        public BishopAttackMechanics(IFieldUnderPinChecker fieldUnderPinChecker, ICheckFinder checkFinder)
        {
            mFieldUnderPinChecker = fieldUnderPinChecker;
            mCheckFinder = checkFinder;
        }
        public void GetBishopsAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            List<Point> allowedMovesList = new();
            foreach (var bishop in fieldsList.Where(x => x.CurrentFigure is Bishop && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
                for (int i = 0; i < 4; i++)
                {
                    allowedMovesList.Add(new Point(bishop.RowIndex, bishop.ColumnIndex));
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

                    mCheckFinder.FindFieldsUnderAttackAndCheck(allowedMovesList, fieldsList, true);
                    mFieldUnderPinChecker.PinFigure(allowedMovesList, fieldsList);

                    allowedMovesList.Clear();
                }
            }
        }
    }
}
