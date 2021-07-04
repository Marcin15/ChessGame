using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IFieldUnderPinChecker
    {
        void PinFigure(List<Point> allowedMovesList, ObservableCollection<IField> fieldsList);
    }
}