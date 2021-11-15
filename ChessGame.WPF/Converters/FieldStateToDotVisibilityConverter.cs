using ChessGame.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ChessGame
{
    public class FieldStateToDotVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FieldState state = (FieldState)value;
            return state == FieldState.MoveState ? Visibility.Visible : (object)Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
