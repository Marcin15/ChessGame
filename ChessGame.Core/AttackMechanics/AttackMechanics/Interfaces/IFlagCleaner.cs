using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IFlagCleaner
    {
        void Clean(ObservableCollection<IField> fieldsList);
    }
}