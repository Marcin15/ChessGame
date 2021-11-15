using System;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class GameConnectionWindowViewModel : BaseViewModel, ICloseGameConnectionWindowService
    {
        private readonly IServerConnection _ServerConnection;
        private readonly IMessageBoxService _MessageBoxService;
        private readonly IShowMainWindowService _ShowMainWindow;
        private readonly MainWindowViewModel _MainWindowViewModel;
        public string ServerIp { get; set; } = "172.26.153.33";
        public ICommand ConnectToTheGameClickCommand { get; set; }
        public ICommand HostTheGameClickCommand { get; set; }
        public Action Close { get; set; }

        public GameConnectionWindowViewModel(IMessageBoxService messageBoxService,
                                             IServerConnection serverConnection,
                                             IShowMainWindowService showMainWindow,
                                             MainWindowViewModel mainWindowViewModel)
        {
            _ServerConnection = serverConnection;
            _MessageBoxService = messageBoxService;
            _ShowMainWindow = showMainWindow;
            _MainWindowViewModel = mainWindowViewModel;

            OnStartUp();
        }

        private void OnStartUp()
        {
            ConnectToTheGameClickCommand = new RelayCommand(ConnectToTheGameClick);
            HostTheGameClickCommand = new RelayCommand(HostTheGameClick);
        }
        public void ConnectToTheGameClick(object obj)
        {
            if(!string.IsNullOrEmpty(ServerIp))
            {
                TcpClientInstance.ServerIp = ServerIp;
                TcpClientInstance.TcpClient = _ServerConnection.ConnectClientToServer();
                Close?.Invoke();
                _ShowMainWindow.Show();

                _MainWindowViewModel.StartReceivingMessages();
            }
            else
            {
                _MessageBoxService.ShowMessage("TextBox is empty", "Error");
            }
        }

        public void HostTheGameClick(object obj)
        {
            TcpServerInstance.TcpListener = TcpServerInstance.StartServer();
            TcpClientInstance.TcpClient = _ServerConnection.ConnectClientToServer(TcpServerInstance.TcpListener);

            Close?.Invoke();
            _ShowMainWindow.Show();

            _MainWindowViewModel.StartReceivingMessages();
        }
    }
}
