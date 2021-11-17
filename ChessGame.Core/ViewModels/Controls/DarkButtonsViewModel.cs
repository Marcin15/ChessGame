using System;

namespace ChessGame.Core
{
    public class DarkButtonsViewModel : BaseViewModel, IField
    {
        private FieldState fieldState;
        private Uri figureImageSource;
        private bool isClicked;
        public FieldState FieldState
        {
            get => fieldState;
            set
            {
                fieldState = value;
                OnPropertyChanged(nameof(FieldState));
            }
        }
        public Uri FigureImageSource
        {
            get
            {
                return figureImageSource;
            }
            set
            {
                figureImageSource = value;
                OnPropertyChanged(nameof(FigureImageSource));
            }
        }
        public bool IsClicked
        {
            get
            {
                return isClicked;
            }
            set
            {
                isClicked = value;

                if (isClicked)
                    this.FieldState = FieldState.ClickedState;
                else
                    this.FieldState = FieldState.EmptyState;
            }
        }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public Piece CurrentFigure { get; set; } = null;
        public bool IsUnderAttack { get; set; } = false;
        public bool IsUnderCheck { get; set; } = false;
        public bool IsUnderPin { get; set; } = false;
        public DarkButtonsViewModel()
        {
            figureImageSource = new Uri(@"/Images/Default.png", UriKind.Relative);
        }
    }
}
