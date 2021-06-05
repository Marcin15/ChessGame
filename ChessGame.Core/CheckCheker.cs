using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ChessGame.Core
{
    public class CheckCheker
    {
        public void Container(ObservableCollection<IField> fieldsList)
        {
            ResetCheckFlag();
            if (CheckChecker(fieldsList))
            {
                RaiseCheckFlag();
                Debug.WriteLine("Szach");
            }
        }
        private bool ResetCheckFlag() => GameInfo.Check = false;
        private void RaiseCheckFlag() => GameInfo.Check = true;
        private bool CheckChecker(ObservableCollection<IField> fieldsList)
        {
            IField kingField = null;
            if (GameInfo.CurrentPlayer == Player.White)
                 kingField = fieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player == Player.White).FirstOrDefault();
            else
                kingField = fieldsList.Where(x => x.CurrentFigure is King && x.CurrentFigure.Player == Player.Black).FirstOrDefault();

            List<Point> alloweMovesList = new List<Point>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    switch (i)
                    {
                        case 0:
                            alloweMovesList.Add(new Point(kingField.RowIndex + j, kingField.ColumnIndex + j));
                            break;
                        case 1:
                            alloweMovesList.Add(new Point(kingField.RowIndex + j, kingField.ColumnIndex - j));
                            break;
                        case 2:
                            alloweMovesList.Add(new Point(kingField.RowIndex - j, kingField.ColumnIndex + j));
                            break;
                        case 3:
                            alloweMovesList.Add(new Point(kingField.RowIndex - j, kingField.ColumnIndex - j));
                            break;
                        case 4:
                            alloweMovesList.Add(new Point(kingField.RowIndex + j, kingField.ColumnIndex));
                            break;
                        case 5:
                            alloweMovesList.Add(new Point(kingField.RowIndex - j, kingField.ColumnIndex));
                            break;
                        case 6:
                            alloweMovesList.Add(new Point(kingField.RowIndex, kingField.ColumnIndex + j));
                            break;
                        case 7:
                            alloweMovesList.Add(new Point(kingField.RowIndex, kingField.ColumnIndex - j));
                            break;
                    }
                }
                foreach (var point in alloweMovesList)
                {
                    var checkField = fieldsList.Where(x => x.RowIndex == point.RowIndex && x.ColumnIndex == point.ColumnIndex)
                                              .FirstOrDefault();

                    if (checkField is not null)
                    {
                        if (checkField.CurrentFigure == null)
                            continue;

                        else if (checkField.CurrentFigure.Player == kingField.CurrentFigure.Player)
                        {
                            alloweMovesList.Clear();
                            break;
                        }
                        else if(checkField.CurrentFigure is Queen && 
                                checkField.CurrentFigure.Player != kingField.CurrentFigure.Player)
                        {
                            fieldsList.Where(x => x.RowIndex == checkField.RowIndex && x.ColumnIndex == checkField.ColumnIndex)
                                        .Select(x => x.IsUnderCheck = true)
                                        .FirstOrDefault();

                            alloweMovesList.Clear();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
