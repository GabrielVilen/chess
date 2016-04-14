using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess.Game;
using Chess.Util;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly int TotalRows = 8, TotalColumns = 8;
        private Player playerWhite, playerBlack;
        private Game.Game game = Game.Game.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame()
        {
            game = Game.Game.GetInstance();

            playerWhite = new Player(white_textBox.Text, Enums.Color.White);
            playerBlack = new Player(black_textBox.Text, Enums.Color.Black);

            game.NewGame(playerWhite, playerBlack);
        }

        private void InitSquares()
        {
            var squares = game.Board.Squares;
            for (int row = 0; row < TotalRows; row++)
            {
                bool evenRow = (row % 2 == 0);
                for (int column = 0; column < TotalColumns; column++)
                {
                    bool evenColumn = (column % 2 == 0);

                    Enums.Color color;
                    if (evenRow && evenColumn || !evenRow && !evenColumn)
                        color = Enums.Color.White;
                    else
                        color = Enums.Color.Black;

                    squares[row, column] = new Square(row + 1, column + 1, color); // start at row and column 1
                }
            }
        }

        private void CreateRectangle()
        {
           // ChessGrid.

            for (int i = 0; i < ChessGrid.Children.; i++)
            {
               // Debug.WriteLine(rec);
            }
            foreach (var child in ChessGrid.Children)
            {
                Rectangle rec = (Rectangle) child;
                Debug.WriteLine(rec);
                //child.

            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRectangle();
            //NewGame();
        }
    }
}
