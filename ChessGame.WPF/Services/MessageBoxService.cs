using ChessGame.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ChessGame
{
    internal class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }
    }
}
