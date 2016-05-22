using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Gui;
using Chess.Util;
using System.Windows.Media;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        private Player playerWhite, playerBlack;
        private Game game;

        public MainWindow()
        {
            game = Game.GetInstance();

            InitializeComponent();
        }

        private void NewGame()
        {
            playerWhite = new Player(white_textBox.Text, Enums.Color.White);
            playerBlack = new Player(black_textBox.Text, Enums.Color.Black);

            game.NewGame(playerWhite, playerBlack);

            ClearGui();
            InitNewBoard();
        }

        // todo: clear clicked etc 
        private void ClearGui()
        {
            foreach (var child in chessGrid.Children)
            {
                Label label = child as Label;
                label.Content = "";
            }
            UpdateGui();
        }

        private void UpdateGui()
        {
            chessGrid.UpdateLayout();
        }


        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)e.Source;

            int column = Grid.GetColumn(label);
            int row = Grid.GetRow(label);

            Debug.WriteLine("label = " + label + "\n  column = " + column + " row = " + row);

            bool isClicked = game.ClickSquare(row, column);

            label.Foreground = isClicked ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Black);
        }

   
        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
