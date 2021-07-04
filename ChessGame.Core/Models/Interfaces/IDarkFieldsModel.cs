using System.Collections.Generic;

namespace ChessGame.Core
{
    public interface IDarkFieldsModel
    {
        List<DarkButtonsViewModel> GetDarkFieldsList();
    }
}