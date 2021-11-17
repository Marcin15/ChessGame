using System;
using System.Threading;
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

        #region Properties
        public string ServerIp { get; set; }
        public ICommand ConnectToTheGameClickCommand { get; set; }
        public ICommand HostTheGameClickCommand { get; set; }
        public ICommand CopyIpClickCommand { get; set; }
        public Action Close { get; set; }
        #endregion

        #region FullProps
        private bool isIPv4Valid = true;
        public bool IsIPv4Valid
        {
            get => isIPv4Valid;
            set
            {
                isIPv4Valid = value;
                OnPropertyChanged(nameof(IsIPv4Valid));
            }
        }

        private string connectBtnContent = "CONNECT";

        public string ConnectBtnContent
        {
            get => connectBtnContent;
            set
            {
                connectBtnContent = value;
                OnPropertyChanged(nameof(ConnectBtnContent));
            }
        }

        private string _hostBtnContent = "HOST GAME";

        public string HostBtnContent
        {
            get => _hostBtnContent;
            set
            {
                _hostBtnContent = value;
                OnPropertyChanged(nameof(HostBtnContent));
            }
        }

        #endregion

        private CancellationTokenSource _source;

        private bool _isConnectionBtnActive = false;
        private bool _isHostBtnActive = false;

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
        public async void ConnectToTheGameClick(object obj)
        {
            IsIPv4Valid = true;

            if (_isHostBtnActive)
            {
                _isHostBtnActive = !_isHostBtnActive;
                CancelTokenSource();
                HostBtnContent = "HOST GAME";
            }

            if (_IPv4Validator.Validate(ServerIp))
            {
                if (!_isConnectionBtnActive)
                {
                    _isConnectionBtnActive = !_isConnectionBtnActive;
                    ConnectBtnContent = "STOP";

                    TcpClientInstance.ServerIp = ServerIp;
                    TcpClientInstance.TcpClient = await _ServerConnection.ConnectClientToServerAsync(CreateCancellationToken());

                    if (TcpClientInstance.TcpClient is not null)
                    {
                        AcceptTcpClient();
                    }
                }
                else
                {
                    _isConnectionBtnActive = !_isConnectionBtnActive;
                    ConnectBtnContent = "CONNECT";
                    CancelTokenSource();
                }
            }
            else
            {
                IsIPv4Valid = false;
            }
        }

        public async void HostTheGameClick(object obj)
        {
            if (_isConnectionBtnActive)
            {
                _isConnectionBtnActive = !_isConnectionBtnActive;
                CancelTokenSource();
                ConnectBtnContent = "CONNECT";
            }

            if (!_isHostBtnActive)
            {
                _isHostBtnActive = !_isHostBtnActive;
                HostBtnContent = "STOP";

                TcpServerInstance.TcpListener = TcpServerInstance.StartServer();
                TcpClientInstance.TcpClient = await _ServerConnection.ConnectClientToServerAsync(TcpServerInstance.TcpListener, CreateCancellationToken());

                if (TcpClientInstance.TcpClient is not null)
                {
                    AcceptTcpClient();
                }
            }
            else
            {
                _isHostBtnActive = !_isHostBtnActive;
                HostBtnContent = "HOST GAME";
                CancelTokenSource();
            }
        }

        public void CopyIpClick(object obj)
        {
            Clipboard.SetText(_ServerIpGetter.ServerIp);
        }

        private void AcceptTcpClient()
        {
            Close?.Invoke();
            _ShowMainWindow.Show();

            _MainWindowViewModel.StartReceivingMessages();
        }

        private CancellationToken CreateCancellationToken()
        {
            _source = new();
            return _source.Token;
        }

        private void CancelTokenSource()
        {
            _source.Cancel();
            _source.Dispose();

            if (TcpServerInstance.TcpListener is not null)
            {
                TcpServerInstance.TcpListener.Stop();
            }
        }
    }
}
