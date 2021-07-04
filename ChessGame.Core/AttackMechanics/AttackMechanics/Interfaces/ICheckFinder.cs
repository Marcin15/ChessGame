using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface ICheckFinder
    {
        void FindFieldsUnderAttackAndCheck(List<Point> potentialMovesList, ObservableCollection<IField> fieldsList, bool isLinearAttackingPiece);
    }
}