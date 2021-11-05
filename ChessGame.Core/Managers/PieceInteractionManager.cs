﻿using System.Collections.ObjectModel;
using System.Linq;

namespace ChessGame.Core
{
    public class PieceInteractionManager : IPieceInteractionManager
    {
        private IField mOldClickedFigure;
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
            if (MoveFigure(clickedField, fieldsList))
            {
                mAttackMechanicContainer.Container(fieldsList);
                SetPawnsPassingFlagToFalse(fieldsList);
                mCheckMateChecker.Check(fieldsList);
                _DataSender.SendData(mOldClickedFigure, clickedField);
                mOldClickedFigure = null;
                ChangePlayer();
            }
        }

        private void ChangePlayer() => GameInfo.CurrentPlayer = (GameInfo.CurrentPlayer == Player.White) ? Player.Black : Player.White;

        private void AssignPreviousClickedFigure(IField clickedField)
        {
            if (clickedField.CurrentFigure is Piece && clickedField.CurrentFigure.Player == GameInfo.CurrentPlayer)
            {
                mOldClickedFigure = clickedField;
            }
            if (clickedField.IsClicked)
                mOldClickedFigure = null;
        }

        private void SetPawnsPassingFlagToFalse(ObservableCollection<IField> fieldsList)
        {
            foreach (var pawn in fieldsList.Where(x => x.CurrentFigure is Pawn && x.CurrentFigure.Player != GameInfo.CurrentPlayer).ToList())
            {
                Pawn somePawn = pawn.CurrentFigure as Pawn;
                somePawn.Passing = false;
            }
        }

        private bool MoveFigure(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            if (mOldClickedFigure is not null &&
                mOldClickedFigure.CurrentFigure is Piece &&
                clickedField != mOldClickedFigure &&
                mOldClickedFigure.CurrentFigure.Player == GameInfo.CurrentPlayer)
            {
                if (mOldClickedFigure.CurrentFigure.Move(mOldClickedFigure, clickedField, fieldsList))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
