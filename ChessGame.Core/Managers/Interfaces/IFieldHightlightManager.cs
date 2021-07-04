using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IFieldHightlightManager
    {
        void Container(IField field, ObservableCollection<IField> fieldsList);
    }
}