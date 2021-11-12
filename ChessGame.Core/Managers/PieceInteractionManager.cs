using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class PieceInteractionManager : IPieceInteractionManager
    {
        private IField _PreviousClickedPiece;
        private readonly IAttackMechanicContainer mAttackMechanicContainer;
        private readonly ICheckMateChecker mCheckMateChecker;
        private readonly IDataSender _DataSender;
        public PieceInteractionManager(IAttackMechanicContainer attackMechanicContainer, 
                                       ICheckMateChecker checkMateChecker,
                                       IDataSender dataSender)
        {
            mAttackMechanicContainer = attackMechanicContainer;
            mCheckMateChecker = checkMateChecker;
            _DataSender = dataSender;
        }
        public void Container(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            AssignPreviousClickedFigure(clickedField);
            if (MoveFigure(_PreviousClickedPiece, clickedField, fieldsList))
            {
                mAttackMechanicContainer.Container(fieldsList);
                SetPawnsPassingFlagToFalse(fieldsList);
                mCheckMateChecker.Check(fieldsList);
                _DataSender.SendData(_PreviousClickedPiece, clickedField);
                _PreviousClickedPiece = null;
                ChangePlayer();
            }
        }
        
        public void Container(IField previousClickedPiece, IField clickedField, ObservableCollection<IField> fieldsList)
        {
            if (MoveFigure(previousClickedPiece, clickedField, fieldsList))
            {
                mAttackMechanicContainer.Container(fieldsList);
                SetPawnsPassingFlagToFalse(fieldsList);
                mCheckMateChecker.Check(fieldsList);
                ChangePlayer();
            }
        }

        private void ChangePlayer() => GameInfo.CurrentPlayer = (GameInfo.CurrentPlayer == Player.White) ? Player.Black : Player.White;

        private void AssignPreviousClickedFigure(IField clickedField)
        {
            if (clickedField.CurrentFigure is Piece && clickedField.CurrentFigure.Player == GameInfo.CurrentPlayer)
            {
                _PreviousClickedPiece = clickedField;
            }
            if (clickedField.IsClicked)
                _PreviousClickedPiece = null;
        }

        private void SetPawnsPassingFlagToFalse(ObservableCollection<IField> fieldsList)
        {
            foreach (var pawn in fieldsList.Where(x => x.CurrentFigure is Pawn && x.CurrentFigure.Player != GameInfo.CurrentPlayer).ToList())
            {
                Pawn somePawn = pawn.CurrentFigure as Pawn;
                somePawn.Passing = false;
            }
        }

        private bool MoveFigure(IField previousClickedPiece, IField clickedField, ObservableCollection<IField> fieldsList)
        {
            if (previousClickedPiece is not null &&
                previousClickedPiece.CurrentFigure is Piece &&
                clickedField != previousClickedPiece )
            {
                if (previousClickedPiece.CurrentFigure.Move(previousClickedPiece, clickedField, fieldsList))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

//                previousClickedPiece.CurrentFigure.Player == GameInfo.CurrentPlayer