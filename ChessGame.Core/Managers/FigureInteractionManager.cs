using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ChessGame.Core
{
    public class FigureInteractionManager : IManager
    {
        private IField mOldClickedFigure;
        public void Container(IField field, ObservableCollection<IField> fieldsList)
        {
            AssignPreviousClickedFigure(field);
            MoveFigure(field, fieldsList);
        }
        private void AssignPreviousClickedFigure(IField clickedField)
        {
            if (clickedField.CurrentFigure is Figure)
            {
                mOldClickedFigure = clickedField;
            }
            if (!clickedField.IsClicked)
                mOldClickedFigure = null;
        }
        private void MoveFigure(IField field, ObservableCollection<IField> fieldsList)
        {
            if(mOldClickedFigure is not null && 
                field.CurrentFigure is not Figure && 
                mOldClickedFigure.CurrentFigure is Figure)
            {
                mOldClickedFigure.CurrentFigure.Move(mOldClickedFigure, field, fieldsList);
                mOldClickedFigure = null;
            }
        }
    }
}
