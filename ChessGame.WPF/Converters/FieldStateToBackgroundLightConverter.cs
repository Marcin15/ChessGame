using ChessGame.Core;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ChessGame
{
    class FieldStateToBackgroundLightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush defaultColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#cccccc");
            SolidColorBrush clickedColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#abbeff");
            SolidColorBrush moveColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#123123");

            FieldState state = (FieldState)value;
            if (state == FieldState.ClickedState)
                return clickedColor;
            //else if (state == FieldState.MoveState)
            //    return moveColor;
            else
                return defaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
