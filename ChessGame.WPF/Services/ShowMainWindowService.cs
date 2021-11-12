using ChessGame.Core;

namespace ChessGame
{
    public class ShowMainWindowService : IShowMainWindowService
    {
        private readonly MainWindow _MainWindow;
        public ShowMainWindowService(MainWindow mainWindow)
        {
            _MainWindow = mainWindow;
        }
        public void Show()
        {
            _MainWindow.Show();
        }
    }
}
