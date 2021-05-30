﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class FieldHightlightManager
    {
        private IField mOldClickedField;
        public void Container(IField field, ObservableCollection<IField> fieldsList, Player currentPlayer)
        {
            IsClickerSwitcher(field);
            HighlightOneField(field);
            AssignPreviousClickedField(field);
            SetEveryFieldStatateToEmpty( fieldsList);
            ShowFiguresAllowedMoves(field, fieldsList, currentPlayer);
        }
        private void IsClickerSwitcher(IField clickedField) => clickedField.IsClicked = !clickedField.IsClicked;
        private void AssignPreviousClickedField(IField clickedField) => mOldClickedField = clickedField;
        private void HighlightOneField(IField clickedField)
        {
            if (mOldClickedField != clickedField &&
                mOldClickedField is not null)
            {
                mOldClickedField.IsClicked = false;
            }
        }
        private void SetEveryFieldStatateToEmpty(ObservableCollection<IField> fieldsList)
        {
            foreach (var field in fieldsList.Where(x => x.FieldState == FieldState.MoveState || x.FieldState == FieldState.CaptureState).ToList())
            {
                field.FieldState = FieldState.EmptyState;
            }
        }
        private void ShowFiguresAllowedMoves(IField field, ObservableCollection<IField> fieldsList, Player currentPlayer)
        {
            if (field.CurrentFigure is Figure && field.IsClicked && field.CurrentFigure.Player == currentPlayer)
            {
                field.CurrentFigure.AllowedMoves(field, fieldsList);
            }
        }
    }
}
