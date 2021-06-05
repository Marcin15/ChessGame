using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public sealed class InitialLocalizationOfFigures
    {
        public static InitialLocalizationHelper[] initialLocalizations =
        {
            new InitialLocalizationHelper(0,0, Player.Black, typeof(Rook).Name),
            new InitialLocalizationHelper(0,1, Player.Black, typeof(Knight).Name),
            new InitialLocalizationHelper(0,2, Player.Black, typeof(Bishop).Name),
            new InitialLocalizationHelper(0,3, Player.Black, typeof(Queen).Name),
            new InitialLocalizationHelper(0,4, Player.Black, typeof(King).Name),
            new InitialLocalizationHelper(0,5, Player.Black, typeof(Bishop).Name),
            new InitialLocalizationHelper(0,6, Player.Black, typeof(Knight).Name),
            new InitialLocalizationHelper(0,7, Player.Black, typeof(Rook).Name),
            new InitialLocalizationHelper(1,0, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,1, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,2, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,3, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,4, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,5, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,6, Player.Black, typeof(Pawn).Name),
            new InitialLocalizationHelper(1,7, Player.Black, typeof(Pawn).Name),

            new InitialLocalizationHelper(7,0, Player.White, typeof(Rook).Name),
            new InitialLocalizationHelper(7,1, Player.White, typeof(Knight).Name),
            new InitialLocalizationHelper(7,2, Player.White, typeof(Bishop).Name),
            new InitialLocalizationHelper(7,3, Player.White, typeof(Queen).Name),
            new InitialLocalizationHelper(7,4, Player.White, typeof(King).Name),
            new InitialLocalizationHelper(7,5, Player.White, typeof(Bishop).Name),
            new InitialLocalizationHelper(7,6, Player.White, typeof(Knight).Name),
            new InitialLocalizationHelper(7,7, Player.White, typeof(Rook).Name),
            new InitialLocalizationHelper(6,0, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,1, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,2, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,3, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,4, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,5, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,6, Player.White, typeof(Pawn).Name),
            new InitialLocalizationHelper(6,7, Player.White, typeof(Pawn).Name)
        };
    }
}
