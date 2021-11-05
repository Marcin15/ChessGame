using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public interface IMessageBoxService
    {
        void ShowMessage(string text, string caption);
    }
}
