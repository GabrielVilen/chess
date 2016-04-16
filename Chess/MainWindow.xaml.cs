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
using System.Data;
using Chess.Pieces;

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
        private DataTable table;

        public MainWindow()
        {
            //  this.DataContext = this;
            InitializeComponent();
            InitSquares();
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
            InitDataTable();

            //var squares = game.Board.Squares;
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

                    Square square = new Square(row + 1, column + 1, color); // start at row and column 1
                    //squares[row, column] = square; 
                    table.Rows[row][column] = square;
                }
            }
           
            chessGrid.DataContext = table;   // todo: datacontext is not set
            this.DataContext = table;
            //dataGrid.DataContext = table;

            Refresh();
            PrintTable();

            TestDataContext();
        } 

        private void TestDataContext()
        {
            Debug.WriteLine("chessGrid.DataContext = " +  chessGrid.DataContext);
            for (int i = 0; i < 5; i++)
            {
                Debug.WriteLine("TestDataContext");
                ((Square)table.Rows[i][i]).CurrPiece = new King(Enums.Color.Black);
            }

            Debug.WriteLine("this.DataContext = " + this.DataContext);
        }

        private void Refresh()
        {
            dataGrid.Items.Refresh();
            dataGrid.UpdateLayout();
        }

        private void PrintTable()
        {
            for (int i = 0; i < TotalRows; i++)
            {
                Debug.WriteLine("");
                for (int j = 0; j < TotalColumns; j++)
                {
                    Debug.Write(" " + ((Square)table.Rows[i][j]).Color);
                }
            }
            Debug.WriteLine("\n");
        }

        private void InitDataTable()
        {
            table = new DataTable();
            for (int row = 0; row < TotalRows; row++)
            {
                table.Columns.Add(row.ToString(), typeof(Square));
            }
            for (int column = 0; column < TotalColumns; column++)
            {
                table.Rows.Add();
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChessGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("ChessGrid_DataContextChanged");
        }

        // todo: continue chessGrid_MouseDown
        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;

            int c = Grid.GetColumn(element);
            int r = Grid.GetRow(element);

            Debug.WriteLine("c = " + c + " r = " + r);
            game.Board.GetSquare(r, c);
        }

        private void CreateRectangle()
        {
            foreach (var child in chessGrid.Children)
            {
                Rectangle rec = (Rectangle)child;
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            // CreateRectangle();
            //NewGame();
        }
    }
}
