using ChessGame.Core;
using System.Windows;

namespace ChessGame
{
    /// <summary>
    /// Logika interakcji dla klasy GameConnectionWindow.xaml
    /// </summary>
    public partial class GameConnectionWindow : Window
    {
        public GameConnectionWindow(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            Loaded += GameConnectionWindow_Loaded;
        }

        private void GameConnectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is ICloseGameConnectionWindowService vm)
            {
                vm.Close = () =>
                {
                    this.Close();
                };
            }
        }
    }
}
