using ChessGame.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessGame
{
    /// <summary>
    /// Logika interakcji dla klasy ChessButtonLight.xaml
    /// </summary>
    public partial class ChessButtonLight : UserControl
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ChessButtonLight));


        public ChessButtonLight()
        {
            InitializeComponent();
        }

        //public Point Position { get; set; }
        //public bool IsClicked { get; set; } = false;
        //public bool IsSiedged { get; set; } = false;
        //public Figure Figure { get; set; } = null;

        //public SolidColorBrush FieldBorderColorDefault { get; private set; } = new SolidColorBrush(Colors.Gray);
        //public SolidColorBrush FieldBackgroundColorDefault { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#cccccc");

        //public SolidColorBrush FieldBorderColor
        //{
        //    get { return (SolidColorBrush)GetValue(FieldBorderColorProperty); }
        //    set { SetValue(FieldBorderColorProperty, value); }
        //}
        //public static readonly DependencyProperty FieldBorderColorProperty =
        //    DependencyProperty.Register("FieldBorderColor", typeof(SolidColorBrush), typeof(ChessButtonLight), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        //public SolidColorBrush FieldBackgroundColor
        //{
        //    get { return (SolidColorBrush)GetValue(FieldBackgroundColorProperty); }
        //    set { SetValue(FieldBackgroundColorProperty, value); }
        //}
        //public static readonly DependencyProperty FieldBackgroundColorProperty =
        //    DependencyProperty.Register("FieldBackgroundColor", typeof(SolidColorBrush), typeof(ChessButtonLight), new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom("#cccccc")));

        //public Uri FieldSource
        //{
        //    get { return (Uri)GetValue(FieldSourceProperty); }
        //    set { SetValue(FieldSourceProperty, value); }
        //}
        //public static readonly DependencyProperty FieldSourceProperty =
        //    DependencyProperty.Register("FieldSource", typeof(Uri), typeof(ChessButtonLight), new PropertyMetadata(new Uri(@"/Images/Default.png", UriKind.Relative)));
    }
}
