using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IRookAttackMechanics
    {
        void GetRooksAttackMechanics(ObservableCollection<IField> fieldsList);
    }
}