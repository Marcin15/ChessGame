using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Core
{
    public class CollectionMerger
    {
        private List<DarkButtonsViewModel> darkFieldsList = new List<DarkButtonsViewModel>(new DarkButtonsModel().DarkButtonsList);
        private List<LightButtonsViewModel> lightFieldsList = new List<LightButtonsViewModel>(new LightButtonsModel().LightButtonsList);

        public List<IField> MergeTwoListIntoOne()
        {
            var mergedList = new List<IField>();

            foreach (var darkField in darkFieldsList)
            {
                mergedList.Add(darkField);
            }
            foreach (var lightField in lightFieldsList)
            {
                mergedList.Add(lightField);
            }

            return mergedList;
        }
    }
}
