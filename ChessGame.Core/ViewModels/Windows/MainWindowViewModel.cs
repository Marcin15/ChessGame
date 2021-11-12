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
        private readonly IDataReceiver _DataReceiver;

        public ObservableCollection<IField> FieldsList { get; set; }
        public ICommand ClickCommand { get; set; }
        public MainWindowViewModel(ICollectionMerger collectionMerger,
            IPieceCreatorFactory pieceCreatorFactory,
            IFieldHightlightManager fieldHightlightManager,
            IPieceInteractionManager pieceInteractionManager,
            IDataReceiver dataReceiver)
        {
            mPieceCreatorFactory = pieceCreatorFactory;
            mCollectionMerger = collectionMerger;
            mFieldHightlightManager = fieldHightlightManager;
            mPieceInteractionManager = pieceInteractionManager;
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

            //Debug.WriteLine("Start");
            //Debug.WriteLine($"{clickedField.RowIndex} {clickedField.ColumnIndex}");
            //Debug.WriteLine("End");
            //Debug.WriteLine("");

            mPieceInteractionManager.Container(clickedField, FieldsList);
            mFieldHightlightManager.Container(clickedField, FieldsList);
        }

        public void StartReceivingMessages()
        {
            var receiveMessage = Task.Factory.StartNew(() => _DataReceiver.ReadMessageAsync(TcpClientInstance.TcpClient, FieldsList));

            Task.WaitAll(receiveMessage);
        }
    }
}
