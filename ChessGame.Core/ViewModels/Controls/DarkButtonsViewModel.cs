using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public class DarkButtonsViewModel : BaseViewModel, IField
    {
        private FieldState mFieldState;
        private Uri mFigureImageSource;
        private bool mIsClicked;
        public FieldState FieldState
        {
            get
            {
                return mFieldState;
            }
            set
            {
                mFieldState = value;
                OnPropertyChanged(nameof(FieldState));
            }
        }
        public Uri FigureImageSource
        {
            get
            {
                return mFigureImageSource;
            }
            set
            {
                mFigureImageSource = value;
                OnPropertyChanged(nameof(FigureImageSource));
            }
        }
        public bool IsClicked
        {
            get
            {
                return mIsClicked;
            }
            set
            {
                mIsClicked = value;

                if (mIsClicked)
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
            mFigureImageSource = new Uri(@"/Images/Default.png", UriKind.Relative);
        }
    }
}
