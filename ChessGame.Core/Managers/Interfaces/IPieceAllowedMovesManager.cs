using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IPieceAllowedMovesManager
    {
        void Invoke(IField field, ObservableCollection<IField> fieldsList);
    }
}