using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IQueensAttackMechanics
    {
        void GetQueensAttackMechanics(ObservableCollection<IField> fieldsList);
    }
}