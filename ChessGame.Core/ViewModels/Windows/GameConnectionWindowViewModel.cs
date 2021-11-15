using System;
using System.Windows;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class GameConnectionWindowViewModel : BaseViewModel, ICloseGameConnectionWindowService
    {
        private readonly IServerConnection _ServerConnection;
        private readonly IServerIpGetter _ServerIpGetter;
        private readonly IIPv4Validator _IPv4Validator;
        private readonly IMessageBoxService _MessageBoxService;
        private readonly IShowMainWindowService _ShowMainWindow;
        private readonly MainWindowViewModel _MainWindowViewModel;
        public string ServerIp { get; set; }
        public ICommand ConnectToTheGameClickCommand { get; set; }
        public ICommand HostTheGameClickCommand { get; set; }
        public ICommand CopyIpClickCommand { get; set; }
        public Action Close { get; set; }

        public GameConnectionWindowViewModel(IMessageBoxService messageBoxService,
                                             IServerConnection serverConnection,
                                             IShowMainWindowService showMainWindow,
                                             IServerIpGetter serverIpGetter,
                                             IIPv4Validator iPv4Validator,
                                             MainWindowViewModel mainWindowViewModel)
        {
            _ServerConnection = serverConnection;
            _MessageBoxService = messageBoxService;
            _ShowMainWindow = showMainWindow;
            _ServerIpGetter = serverIpGetter;
            _IPv4Validator = iPv4Validator;
            _MainWindowViewModel = mainWindowViewModel;

            OnStartUp();
        }

        private void OnStartUp()
        {
            ConnectToTheGameClickCommand = new RelayCommand(ConnectToTheGameClick);
            HostTheGameClickCommand = new RelayCommand(HostTheGameClick);
            CopyIpClickCommand = new RelayCommand(CopyIpClick);
        }
        public void ConnectToTheGameClick(object obj)
        {
            if (_IPv4Validator.Validate(ServerIp))
            {
                TcpClientInstance.ServerIp = ServerIp;
                TcpClientInstance.TcpClient = _ServerConnection.ConnectClientToServer();
                Close?.Invoke();
                _ShowMainWindow.Show();

                _MainWindowViewModel.StartReceivingMessages();
            }
            else
            {
                _MessageBoxService.ShowMessage("IP Address is not valid", "Error");
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

        public void CopyIpClick(object obj)
        {
            Clipboard.SetText(_ServerIpGetter.ServerIp);
        }
    }
}
