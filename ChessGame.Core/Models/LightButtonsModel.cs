using System.Collections.Generic;

namespace ChessGame.Core
{
    class LightButtonsModel
    {
        public List<LightButtonsViewModel> LightButtonsList = new List<LightButtonsViewModel>();

        private int rowIndex = 0;
        private int columnIndex = 0;
        public LightButtonsModel()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            columnIndex = j;
                            rowIndex = i;
                        }
                        else
                            continue;
                    }
                    else if (i % 2 == 1)
                    {
                        if (j % 2 == 1)
                        {
                            columnIndex = j;
                            rowIndex = i;
                        }
                        else
                            continue;
                    }
                    LightButtonsList.Add(new LightButtonsViewModel
                    {
                        RowIndex = rowIndex,
                        ColumnIndex = columnIndex
                    });
                }
            }
        }
    }
}
