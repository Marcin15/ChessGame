using System.Collections.Generic;

namespace ChessGame.Core
{
    public interface ILightFieldsModel
    {
        List<LightButtonsViewModel> GetLightFieldsList();
    }
}