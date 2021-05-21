using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IManager
    {
        void Container(IField field, ObservableCollection<IField> fieldList);
    }
}