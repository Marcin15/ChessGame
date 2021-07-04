using System.Collections.Generic;

namespace ChessGame.Core
{
    public interface ICollectionMerger
    {
        List<IField> MergeTwoListIntoOne();
    }
}