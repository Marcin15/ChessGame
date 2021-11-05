using ChessGame.Core;
using System;
using System.Collections.Generic;
using System.Text;

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
