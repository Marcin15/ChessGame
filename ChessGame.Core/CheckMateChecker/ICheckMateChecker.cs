using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface ICheckMateChecker
    {
        void Check(ObservableCollection<IField> fieldsList);
    }
}