using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class FigureInteractionManager : IManager
    {
        private IField mOldClickedFigure;
        private bool mTestBoolVar = false;
        private readonly FieldUnderAttackChecker mAttackChecker = new FieldUnderAttackChecker();
        public void Container(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            AssignPreviousClickedFigure(clickedField);
            if(MoveFigure(clickedField, fieldsList))
            {
                mAttackChecker.Container(fieldsList);
                ChangePlayer();
                //mCheckCheker.Container(fieldsList);
            }
            //if(MoveFigure(clickedField, fieldsList) && mTestBoolVar)
            //SetPawnPassingToFalse(fieldsList);
            //SetBool();
        }

        private void ChangePlayer() => GameInfo.CurrentPlayer = (GameInfo.CurrentPlayer == Player.White) ? Player.Black : Player.White;

        private void AssignPreviousClickedFigure(IField clickedField)
        {
            if (clickedField.CurrentFigure is Figure && clickedField.CurrentFigure.Player == GameInfo.CurrentPlayer)
            {
                mOldClickedFigure = clickedField;
            }
            if (clickedField.IsClicked)
                mOldClickedFigure = null;
        }

        private void SetPawnsPassingFlagToFalse(ObservableCollection<IField> fieldsList)
        {
            foreach (var pawn in fieldsList.Where(x => x.CurrentFigure is Pawn).ToList())
            {
                Pawn x = pawn.CurrentFigure as Pawn;
                x.Passing = false;
            }
        }

        private void SomeMethod() => mTestBoolVar = !mTestBoolVar;

        private bool MoveFigure(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            if (mOldClickedFigure is not null &&
                mOldClickedFigure.CurrentFigure is Figure &&
                clickedField != mOldClickedFigure &&
                mOldClickedFigure.CurrentFigure.Player == GameInfo.CurrentPlayer)
            {
                if (mOldClickedFigure.CurrentFigure.Move(mOldClickedFigure, clickedField, fieldsList))
                {
                    mOldClickedFigure = null;
                    return true;
                }
            }
            return false;
        }
    }
}
