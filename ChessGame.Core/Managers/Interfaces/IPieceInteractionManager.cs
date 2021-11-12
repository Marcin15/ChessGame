using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IPieceInteractionManager
    {
        void Container(IField clickedField, ObservableCollection<IField> fieldsList);
        void Container(IField previousClickedPiece, IField clickedField, ObservableCollection<IField> fieldsList);
    }
}