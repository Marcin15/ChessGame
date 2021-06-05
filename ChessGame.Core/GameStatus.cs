using System;

namespace ChessGame.Core
{
    public static class GameInfo
    {
        public static Player CurrentPlayer { get; set; } = Player.White;
        public static bool Check { get; set; } = false;
    }
}
