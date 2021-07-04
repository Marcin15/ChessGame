using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public class CollectionMerger : ICollectionMerger
    {
        private readonly List<DarkButtonsViewModel> mDarkFieldsList;
        private readonly List<LightButtonsViewModel> mLightFieldsList;

        public CollectionMerger(IDarkFieldsModel darkFieldsModel, ILightFieldsModel lightFieldsModel)
        {
            mDarkFieldsList = new(darkFieldsModel.GetDarkFieldsList());
            mLightFieldsList = new(lightFieldsModel.GetLightFieldsList());
        }

        public List<IField> MergeTwoListIntoOne()
        {
            var mergedList = new List<IField>();

            foreach (var darkField in mDarkFieldsList)
            {
                mergedList.Add(darkField);
            }
            foreach (var lightField in mLightFieldsList)
            {
                mergedList.Add(lightField);
            }

            return mergedList;
        }
    }
}
