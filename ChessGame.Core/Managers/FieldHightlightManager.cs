using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public class FieldHightlightManager : IFieldHightlightManager
    {
        private IField _PreviousClickedField;
        public void Invoke(IField field, ObservableCollection<IField> fieldsList)
        {
            IsClickerSwitcher(field);
            HighlightOneField(field);
            AssignPreviousClickedField(field);
        }
        private void IsClickerSwitcher(IField clickedField)
        {
            if (clickedField.FieldState is not (FieldState.MoveState or FieldState.CaptureState))
            {
                clickedField.IsClicked = !clickedField.IsClicked;
            }
        }
        private void AssignPreviousClickedField(IField clickedField) => _PreviousClickedField = clickedField;
        private void HighlightOneField(IField clickedField)
        {
            if (_PreviousClickedField != clickedField &&
                _PreviousClickedField is not null)
            {
                _PreviousClickedField.IsClicked = false;
            }
        }
    }
}
