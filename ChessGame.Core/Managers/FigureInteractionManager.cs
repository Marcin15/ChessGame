using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class FigureInteractionManager : IManager
    {
        private IField mOldClickedFigure;
        private bool mTestBoolVar = false;
        private FieldUnderAttackChecker attackChecker = new FieldUnderAttackChecker();
        public void Container(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            AssignPreviousClickedFigure(clickedField);
            if(MoveFigure(clickedField, fieldsList))
            {
                attackChecker.Container(fieldsList);
                ChangePlayer();
            }
            //if(MoveFigure(clickedField, fieldsList) && mTestBoolVar)
            //SetPawnPassingToFalse(fieldsList);
            //SetBool();
        }

        private void ChangePlayer() 
        {
            if (GameStatus.CurrentPlayer == Player.White)
                GameStatus.CurrentPlayer = Player.Black;
            else if (GameStatus.CurrentPlayer == Player.Black)
                GameStatus.CurrentPlayer = Player.White;
        }

        private void AssignPreviousClickedFigure(IField clickedField)
        {
            if (clickedField.CurrentFigure is Figure && clickedField.CurrentFigure.Player == GameStatus.CurrentPlayer)
            {
                mOldClickedFigure = clickedField;
            }
            if (clickedField.IsClicked)
                mOldClickedFigure = null;
        }

        private void SetPawnPassingToFalse(ObservableCollection<IField> fieldsList)
        {
            foreach (var pawn in fieldsList.Where(x => x.CurrentFigure is Pawn).ToList())
            {
                Pawn x = pawn.CurrentFigure as Pawn;
                x.Passing = false;
            }
        }

        private void SetBool() => mTestBoolVar = !mTestBoolVar;

        private bool MoveFigure(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            if (mOldClickedFigure is not null &&
                mOldClickedFigure.CurrentFigure is Figure &&
                clickedField != mOldClickedFigure &&
                mOldClickedFigure.CurrentFigure.Player == GameStatus.CurrentPlayer)
            {
                if (mOldClickedFigure.CurrentFigure.Move(mOldClickedFigure, clickedField, fieldsList))
                {
                    mOldClickedFigure = null;
                    return true;
                }
                else return false;
            }
            return false;
        }
    }
}
