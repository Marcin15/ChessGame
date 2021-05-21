using System.Collections.Generic;

namespace ChessGame.Core
{
    public class DarkButtonsModel
    {
        public List<DarkButtonsViewModel> DarkButtonsList = new List<DarkButtonsViewModel>();

        private int rowIndex = 0;
        private int columnIndex = 0;
        public DarkButtonsModel()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 1)
                        {
                            columnIndex = j;
                            rowIndex = i;
                        }
                        else
                            continue;
                    }
                    else if (i % 2 == 1)
                    {
                        if (j % 2 == 0)
                        {
                            columnIndex = j;
                            rowIndex = i;
                        }
                        else
                            continue;
                    }

                    DarkButtonsList.Add(new DarkButtonsViewModel
                    {
                        RowIndex = rowIndex,
                        ColumnIndex = columnIndex
                    });
                }
            }
        }
    }
}

