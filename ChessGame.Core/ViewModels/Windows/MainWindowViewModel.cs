using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static ICollectionMerger mCollectionMerger;
        private readonly IPieceCreatorFactory mPieceCreatorFactory;
        private readonly IFieldHightlightManager mFieldHightlightManager;
        private readonly IPieceInteractionManager mPieceInteractionManager;
        private readonly IPieceAllowedMovesManager _PieceAllowedMovesManager;
        private readonly IDataReceiver _DataReceiver;

        public ObservableCollection<IField> FieldsList { get; set; }
        public ICommand ClickCommand { get; set; }
        public MainWindowViewModel(ICollectionMerger collectionMerger,
            IPieceCreatorFactory pieceCreatorFactory,
            IFieldHightlightManager fieldHightlightManager,
            IPieceInteractionManager pieceInteractionManager,
            IPieceAllowedMovesManager pieceAllowedMovesManager,
            IDataReceiver dataReceiver)
        {
            mPieceCreatorFactory = pieceCreatorFactory;
            mCollectionMerger = collectionMerger;
            mFieldHightlightManager = fieldHightlightManager;
            mPieceInteractionManager = pieceInteractionManager;
            _PieceAllowedMovesManager = pieceAllowedMovesManager;
            _DataReceiver = dataReceiver;

            OnStartUp();
        }
        private void OnStartUp()
        {
            FieldsList = new ObservableCollection<IField>(mCollectionMerger.MergeTwoListIntoOne());
            ClickCommand = new RelayCommand(Click);
            mPieceCreatorFactory.Create(new List<IField>(FieldsList));
        }
        public void Click(object obj)
        {
            var clickedField = obj as IField;

            mFieldHightlightManager.Invoke(clickedField, FieldsList);
            mPieceInteractionManager.Invoke(clickedField, FieldsList);
            _PieceAllowedMovesManager.Invoke(clickedField, FieldsList);

            Debug.WriteLine(clickedField.FieldState.ToString());
        }
        public void StartReceivingMessages()
        {
            var receiveMessage = Task.Factory.StartNew(() => _DataReceiver.ReadMessageAsync(TcpClientInstance.TcpClient, FieldsList));

            Task.WaitAll(receiveMessage);
        }
    }
}
