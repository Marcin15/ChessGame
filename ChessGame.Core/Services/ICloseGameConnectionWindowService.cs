using System;

namespace ChessGame.Core
{
    public interface ICloseGameConnectionWindowService
    {
        Action Close { get; set; }
    }
}
