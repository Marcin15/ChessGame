using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class QueensAttackMechanics
    {
        private ObservableCollection<IField> mFieldsList;

        public QueensAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;

            GetQueensAttackMechanics();
        }
        public void GetQueensAttackMechanics()
        {
            var fieldUnderPinChecker = new FieldUnderPinChecker(mFieldsList);
            var checkFinder = new CheckFinder(mFieldsList);

            List<Point> allowedMovesList = new();
            foreach (var queen in mFieldsList.Where(x => x.CurrentFigure is Queen && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
                for (int i = 0; i < 8; i++)
                {
                    allowedMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex));
                    for (int j = 1; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                allowedMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex + j));
                                break;
                            case 1:
                                allowedMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex - j));
                                break;
                            case 2:
                                allowedMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex + j));
                                break;
                            case 3:
                                allowedMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex - j));
                                break;
                            case 4:
                                allowedMovesList.Add(new Point(queen.RowIndex + j, queen.ColumnIndex));
                                break;
                            case 5:
                                allowedMovesList.Add(new Point(queen.RowIndex - j, queen.ColumnIndex));
                                break;
                            case 6:
                                allowedMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex + j));
                                break;
                            case 7:
                                allowedMovesList.Add(new Point(queen.RowIndex, queen.ColumnIndex - j));
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
