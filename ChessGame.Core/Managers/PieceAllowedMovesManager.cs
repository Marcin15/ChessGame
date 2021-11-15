using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class PieceAllowedMovesManager : IPieceAllowedMovesManager
    {
        public void Invoke(IField field, ObservableCollection<IField> fieldsList)
        {
            SetEveryFieldStatateToEmpty(fieldsList);
            ShowFiguresAllowedMoves(field, fieldsList);
        }

        private void SetEveryFieldStatateToEmpty(ObservableCollection<IField> fieldsList)
        {
            foreach (var field in fieldsList.Where(x => x.FieldState is FieldState.MoveState or FieldState.CaptureState).ToList())
            {
                field.FieldState = FieldState.EmptyState;
            }
        }
        private void ShowFiguresAllowedMoves(IField field, ObservableCollection<IField> fieldsList)
        {
            if (field.CurrentFigure is Piece && field.IsClicked && field.CurrentFigure.Player == GameInfo.CurrentPlayer)
            {
                field.CurrentFigure.GetAllowedMovesOfCurrentClickedPiece(field, fieldsList);
            }
        }
    }
}
