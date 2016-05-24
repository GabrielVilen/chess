using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Gui;
using Chess.Util;
using System.Windows.Media;
using System;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        private Player pWhite, pBlack;
        private Game game;
        private Square currSquare;
        private Label currLabel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame()
        {
            Debug.WriteLine("NEW GAME");
            pWhite = new Player(white_textBox.Text, Enums.Color.White);
            pBlack = new Player(black_textBox.Text, Enums.Color.Black);

            if(game == null)
            {
                game = Game.GetInstance();
            }
            this.game = game.NewGame(pWhite, pBlack);

            //ClearGui();
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
            if (game == null) return;

            Label newLabel = (Label)e.Source;
            int column = Grid.GetColumn(newLabel);
            int row = Grid.GetRow(newLabel);

            if (currSquare != null && currLabel != null)
            {
                if(currSquare.IsClicked())
                {
                    game.TryMove(currSquare, row, column); 
                }
                Click(currSquare, currLabel);
            } 
            currSquare = game.GetSquare(row, column);
            currLabel = newLabel;

            Click(currSquare, currLabel);
            
            UpdateGui();
        }

        private void Click(Square currSquare, Label label)
        {
            bool isClicked = currSquare.Click();
            label.Foreground = isClicked ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.Black);
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
