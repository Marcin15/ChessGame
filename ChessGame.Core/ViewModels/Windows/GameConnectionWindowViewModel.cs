using System;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class GameConnectionWindowViewModel : BaseViewModel, ICloseGameConnectionWindowService
    {
        private readonly IMessageBoxService _MessageBoxService;
        private readonly IServerConnection _ServerConnection;
        private readonly IShowMainWindowService _ShowMainWindow;
        public string ServerIp { get; set; }
        public ICommand ConnectToTheGameClickCommand { get; set; }
        public ICommand HostTheGameClickCommand { get; set; }
        public Action Close { get; set; }

        public GameConnectionWindowViewModel(IMessageBoxService messageBoxService,
                                             IServerConnection serverConnection,
                                             IShowMainWindowService showMainWindow)
        {
            _MessageBoxService = messageBoxService;
            _ServerConnection = serverConnection;
            _ShowMainWindow = showMainWindow;

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
            }
            else
            {
                _MessageBoxService.ShowMessage("TextBox is empty", "Error");
            }
        }

        public void HostTheGameClick(object obj)
        {

        }
    }
}
