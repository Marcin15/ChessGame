using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IKingsAttacksMechanics
    {
        void GetKingsAttackMechanics(ObservableCollection<IField> fieldsList);
    }
}