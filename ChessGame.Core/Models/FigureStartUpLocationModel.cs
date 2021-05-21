using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public class FigureStartUpLocationModel
    {
        public static FiguresStartUpLocation[] startUpLocations =
        {
            new FiguresStartUpLocation(0,0, Player.Black, typeof(Rook).Name),
            new FiguresStartUpLocation(0,1, Player.Black, typeof(Knight).Name),
            new FiguresStartUpLocation(0,2, Player.Black, typeof(Bishop).Name),
            new FiguresStartUpLocation(0,3, Player.Black, typeof(Queen).Name),
            new FiguresStartUpLocation(0,4, Player.Black, typeof(King).Name),
            new FiguresStartUpLocation(0,5, Player.Black, typeof(Bishop).Name),
            new FiguresStartUpLocation(0,6, Player.Black, typeof(Knight).Name),
            new FiguresStartUpLocation(0,7, Player.Black, typeof(Rook).Name),
            new FiguresStartUpLocation(1,0, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,1, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,2, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,3, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,4, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,5, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,6, Player.Black, typeof(Pawn).Name),
            new FiguresStartUpLocation(1,7, Player.Black, typeof(Pawn).Name),

            new FiguresStartUpLocation(7,0, Player.White, typeof(Rook).Name),
            new FiguresStartUpLocation(7,1, Player.White, typeof(Knight).Name),
            new FiguresStartUpLocation(7,2, Player.White, typeof(Bishop).Name),
            new FiguresStartUpLocation(7,3, Player.White, typeof(Queen).Name),
            new FiguresStartUpLocation(7,4, Player.White, typeof(King).Name),
            new FiguresStartUpLocation(7,5, Player.White, typeof(Bishop).Name),
            new FiguresStartUpLocation(7,6, Player.White, typeof(Knight).Name),
            new FiguresStartUpLocation(7,7, Player.White, typeof(Rook).Name),
            new FiguresStartUpLocation(6,0, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,1, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,2, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,3, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,4, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,5, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,6, Player.White, typeof(Pawn).Name),
            new FiguresStartUpLocation(6,7, Player.White, typeof(Pawn).Name),
        };
    }
}
