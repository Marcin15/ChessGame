using System.Collections.Generic;

namespace ChessGame.Core
{
    interface IFigureCreator
    {
        void Create(List<IField> fields);
    }
}