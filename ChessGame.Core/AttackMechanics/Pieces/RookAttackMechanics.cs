using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class RookAttackMechanics
    {
        private ObservableCollection<IField> mFieldsList;

        public RookAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;
            GetRookAttackMechanics();
        }
        public void GetRookAttackMechanics()
        {
            var fieldUnderPinChecker = new FieldUnderPinChecker(mFieldsList);
            var checkFinder = new CheckFinder(mFieldsList);

            List<Point> allowedMovesList = new();

            foreach (var rook in mFieldsList.Where(x => x.CurrentFigure is Rook && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
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

                    checkFinder.RaiseIsUnderAttackFlag(allowedMovesList);
                    fieldUnderPinChecker.PinFigure(allowedMovesList);

                    allowedMovesList.Clear();
                }
            }
        }
    }
}
