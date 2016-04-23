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
using Chess.Gui;
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
        private DataTable table;
        private Game game;

        public MainWindow()
        {
            game = Game.GetInstance();

            InitializeComponent();           
        }

        private void NewGame()
        {
            InitDataTable();
            InitSquares();

            playerWhite = new Player(white_textBox.Text, Enums.Color.White);
            playerBlack = new Player(black_textBox.Text, Enums.Color.Black);

            game.NewGame(playerWhite, playerBlack);
            
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

        private void InitSquares()
        {
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
                   
                    Square square = new Square(row, column, color); // start at row and column +1 ?


                    table.Rows[row][column] = square;     
                }
            }
            game.table = table;
            pieceDataGrid.ItemsSource = table.DefaultView;

            InitPieces();
        }

        private void InitPieces()
        {
            game.AddPieceToSquare(new King(Enums.Color.Black), 1, 3);
            game.AddPieceToSquare(new King(Enums.Color.White), 6, 4);
            //  ((Square) table.Rows[0][4]).CurrPiece = new King(Enums.Color.Black);
            //  ((Square) table.Rows[6][5]).CurrPiece = new King(Enums.Color.White);
        }

        private void pieceDataGrid_MouseDown(object sender, SelectionChangedEventArgs e)
        {

        }

        private void pieceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ChessGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("ChessGrid_DataContextChanged");
        }

        private void pieceDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("pieceDataGrid_MouseDown");
        }

        // todo: continue chessGrid_MouseDown
        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;

            int column = Grid.GetColumn(element);
            int row = Grid.GetRow(element);

            Debug.WriteLine("column = " + column + " row = " + row);

            if (game.ClickSquare(row, column))
            {

                // todo: mark piece on gui
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
