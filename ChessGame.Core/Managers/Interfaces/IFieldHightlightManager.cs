﻿using System.Collections.ObjectModel;

namespace ChessGame.Core
{
    public interface IFieldHightlightManager
    {
        void Invoke(IField field, ObservableCollection<IField> fieldsList);
    }
}