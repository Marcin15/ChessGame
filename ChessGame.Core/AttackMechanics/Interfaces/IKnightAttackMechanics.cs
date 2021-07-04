using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IKnightAttackMechanics
    {
        void GetKnightsAttackMechanics(ObservableCollection<IField> fieldsList);
    }
}