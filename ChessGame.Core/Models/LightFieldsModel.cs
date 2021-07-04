using System.Collections.Generic;

namespace ChessGame.Core
{
    public class LightFieldsModel : ILightFieldsModel
    {
        private int rowIndex = 0;
        private int columnIndex = 0;

        public List<LightButtonsViewModel> GetLightFieldsList()
        {
            List<LightButtonsViewModel> lightButtonsList = new();

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
                    lightButtonsList.Add(new LightButtonsViewModel
                    {
                        RowIndex = rowIndex,
                        ColumnIndex = columnIndex
                    });
                }
            }

            return lightButtonsList;
        }
    }
}
