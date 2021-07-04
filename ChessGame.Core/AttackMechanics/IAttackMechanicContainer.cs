using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IAttackMechanicContainer
    {
        void Container(ObservableCollection<IField> fieldsList);
    }
}