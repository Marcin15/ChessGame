using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class PawnsAttackMechanics
    {
        private ObservableCollection<IField> mFieldsList;

        public PawnsAttackMechanics(ObservableCollection<IField> fieldsList)
        {
            mFieldsList = fieldsList;

            GetQueensAttackMechanics();
        }
        public void GetQueensAttackMechanics()
        {
            var checkFinder = new CheckFinder(mFieldsList);

            List<Point> potentialMovesList = null;

            foreach (var pawn in mFieldsList.Where(x => x.CurrentFigure is Pawn && x.CurrentFigure.Player == GameInfo.CurrentPlayer).ToList())
            {
                List<Point> whitePawnAttackMoves = new()
                {
                    new Point(pawn.RowIndex, pawn.ColumnIndex),
                    new Point(pawn.RowIndex - 1, pawn.ColumnIndex + 1),
                    new Point(pawn.RowIndex - 1, pawn.ColumnIndex - 1)
                };

                List<Point> blackPawnAttackMoves = new()
                {
                    new Point(pawn.RowIndex, pawn.ColumnIndex),
                    new Point(pawn.RowIndex + 1, pawn.ColumnIndex + 1),
                    new Point(pawn.RowIndex + 1, pawn.ColumnIndex - 1)
                };

                if (pawn.CurrentFigure.Player == Player.Black)
                {
                    potentialMovesList = blackPawnAttackMoves;
                }
                else if (pawn.CurrentFigure.Player == Player.White)
                {
                    potentialMovesList = whitePawnAttackMoves;
                }
                checkFinder.RaiseIsUnderAttackFlag(potentialMovesList, true);
            }
        }
    }
}
