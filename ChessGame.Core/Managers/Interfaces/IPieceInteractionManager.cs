using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IPieceInteractionManager
    {
        void Invoke(IField clickedField, ObservableCollection<IField> fieldsList);
        void Invoke(IField previousClickedPiece, IField clickedField, ObservableCollection<IField> fieldsList);
    }
}