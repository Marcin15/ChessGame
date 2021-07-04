using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IPawnsAttackMechanics
    {
        void GetPawnsAttackMechanics(ObservableCollection<IField> fieldsList);
    }
}