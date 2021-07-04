using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class RookAttackMechanics : IRookAttackMechanics
    {
        private readonly IFieldUnderPinChecker mFieldUnderPinChecker;
        private readonly ICheckFinder mCheckFinder;
        public RookAttackMechanics(IFieldUnderPinChecker fieldUnderPinChecker, ICheckFinder checkFinder)
        {
            mFieldUnderPinChecker = fieldUnderPinChecker;
            mCheckFinder = checkFinder;
        }
        public void GetRooksAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            List<Point> allowedMovesList = new();

            foreach (var rook in fieldsList.Where(x => x.CurrentFigure is Rook && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
                for (int i = 0; i < 4; i++)
                {
                    allowedMovesList.Add(new Point(rook.RowIndex, rook.ColumnIndex));
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

                    mFieldUnderPinChecker.PinFigure(allowedMovesList, fieldsList);
                    mCheckFinder.FindFieldsUnderAttackAndCheck(allowedMovesList, fieldsList, true);

                    allowedMovesList.Clear();
                }
            }
        }
    }
}
