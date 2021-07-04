using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IBishopAttackMechanics
    {
        void GetBishopsAttackMechanics(ObservableCollection<IField> fieldsList);
    }
}