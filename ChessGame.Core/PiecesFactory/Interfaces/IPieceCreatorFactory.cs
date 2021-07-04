using System.Collections.Generic;

namespace ChessGame.Core
{
    public interface IPieceCreatorFactory
    {
        void Create(List<IField> fields);
    }
}