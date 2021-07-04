using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public interface IFigureFactory
    {
        Piece GetFigure();
    }
}
