using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static ICollectionMerger mCollectionMerger;
        private readonly IPieceCreatorFactory mPieceCreatorFactory;
        private readonly IFieldHightlightManager mFieldHightlightManager;
        private readonly IPieceInteractionManager mPieceInteractionManager;

        public ObservableCollection<IField> FieldsList { get; set; }
        public ICommand ClickCommand { get; set; }
        public MainWindowViewModel(ICollectionMerger collectionMerger, 
            IPieceCreatorFactory pieceCreatorFactory, 
            IFieldHightlightManager fieldHightlightManager, 
            IPieceInteractionManager pieceInteractionManager)
        {
            mPieceCreatorFactory = pieceCreatorFactory;
            mCollectionMerger = collectionMerger;
            mFieldHightlightManager = fieldHightlightManager;
            mPieceInteractionManager = pieceInteractionManager;

            FieldsList = new ObservableCollection<IField>(mCollectionMerger.MergeTwoListIntoOne());
            ClickCommand = new RelayCommand(Click);
            mPieceCreatorFactory.Create(new List<IField>(FieldsList));
        }
        public void Click(object obj)
        {
            var clickedField = obj as IField;

            mPieceInteractionManager.Container(clickedField, FieldsList);
            mFieldHightlightManager.Container(clickedField, FieldsList);

            //Debug.WriteLine(clickedField.CurrentFigure?.IsPinned);
        }
    }
}
