using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class CheckMateChecker : ICheckMateChecker
    {
        public void Check(ObservableCollection<IField> fieldsList)
        {
            int counter = 0;
            foreach (var fields in fieldsList.Where(x => x.CurrentFigure is Piece && x.CurrentFigure.Player != GameInfo.CurrentPlayer).ToList())
            {
                fields.CurrentFigure.GetAllowedMovesOfCurrentClickedFigure(fields, fieldsList);

                counter += fields.CurrentFigure.AllowedMovesCounter;

                if (counter != 0)
                    break;
            }
            if (counter == 0)
                CheckMate(fieldsList);
        }

        private void CheckMate(ObservableCollection<IField> fieldsList)
        {
            fieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player != GameInfo.CurrentPlayer).Select(x => x.FieldState = FieldState.MateState).FirstOrDefault();
        }
    }
}
