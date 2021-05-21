using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class FieldHightlightManager : IManager
    {
        private IField mOldClickedField;
        public void Container(IField field, ObservableCollection<IField> fieldsList)
        {
            IsClickerSwitcher(field);
            HighlightOneField(field);
            AssignPreviousClickedField(field);
            SetEveryFieldStatateToEmpty(field, fieldsList);
            ShowFiguresAllowedMoves(field, fieldsList);
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
        private void SetEveryFieldStatateToEmpty(IField clickedField, ObservableCollection<IField> fieldsList)
        {
            foreach (var field in fieldsList.Where(x => x.FieldState == FieldState.MoveState).ToList())
            {
                field.FieldState = FieldState.EmptyState;
            }
        }
        private void ShowFiguresAllowedMoves(IField field, ObservableCollection<IField> fieldsList)
        {
            if (field.CurrentFigure is Figure && field.IsClicked)
            {
                field.CurrentFigure.AllowedMoves(field, fieldsList);
            }
        }
    }
}
