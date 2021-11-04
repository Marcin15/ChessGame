using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static ICollectionMerger mCollectionMerger;
        private readonly IPieceCreatorFactory mPieceCreatorFactory;
        private readonly IFieldHightlightManager mFieldHightlightManager;
        private readonly IPieceInteractionManager mPieceInteractionManager;
        private readonly IServerConnection _ServerConnection;

        private TcpClient _Client;

        public ObservableCollection<IField> FieldsList { get; set; }
        public ICommand ClickCommand { get; set; }
        public MainWindowViewModel(ICollectionMerger collectionMerger, 
            IPieceCreatorFactory pieceCreatorFactory, 
            IFieldHightlightManager fieldHightlightManager, 
            IPieceInteractionManager pieceInteractionManager,
            IServerConnection serverConnection)
        {
            mPieceCreatorFactory = pieceCreatorFactory;
            mCollectionMerger = collectionMerger;
            mFieldHightlightManager = fieldHightlightManager;
            mPieceInteractionManager = pieceInteractionManager;
            _ServerConnection = serverConnection;

            Start();
        }
        public void Click(object obj)
        {
            var clickedField = obj as IField;

            mPieceInteractionManager.Container(clickedField, FieldsList, _Client);
            mFieldHightlightManager.Container(clickedField, FieldsList);
        }

        private void Start()
        {
            FieldsList = new ObservableCollection<IField>(mCollectionMerger.MergeTwoListIntoOne());
            ClickCommand = new RelayCommand(Click);
            mPieceCreatorFactory.Create(new List<IField>(FieldsList));
            _Client = _ServerConnection.ConnectClientToServer();
        }
    }
}
