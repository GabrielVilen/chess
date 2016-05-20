using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Gui;
using Chess.Util;
using System.Data;

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
            // todo: create setup pieces factory
            BoardFactory.SetupNewBoard(game);

            //  ((Square) table.Rows[0][4]).CurrPiece = new King(Enums.Color.Black);
            //  ((Square) table.Rows[6][5]).CurrPiece = new King(Enums.Color.White);
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


            DataGrid grid = sender as DataGrid;
            //object item = dg.Items[rowIndex];
            var selectedRow = grid.GetSelectedRow();

            var columnCell = grid.GetCell(selectedRow, 0); //  only works when using FullRows selection mode
            Debug.WriteLine("column cell = " + columnCell);  //  only works when using FullRows selection mode
            Debug.WriteLine(" grid.SelectedValue = " + grid.SelectedValue);


            Square square = (Square)grid.SelectedItems;


            //Debug.WriteLine(columnCell.GetValue(de));
            //            DataGridCellInfo cell = grid.SelectedCells[0];
             
            //((DataRowView)cell.Item).Row.ItemArray[];


            //  Debug.WriteLine(((DataGridTextColumn)cell.Column).Binding);

            // Debug.WriteLine(dg.CurrentCell.Item.GetType().ToString());
            //IList rows = dg.SelectedItems; // todo is empty , size = 0





            //DataRowView row = (DataRowView)dg.SelectedItems[0];

            //Debug.WriteLine("Selected items: ");
            //for (int i = 0; i < rows.Count; i++)
            //{
            //    Debug.WriteLine(rows[i]);
            //}

        }
    

        private void pieceDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

 
        // todo: continue chessGrid_MouseDown
        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("chessGrid_MouseDown");
            var element = (UIElement)e.Source;

            int column = Grid.GetColumn(element);
            int row = Grid.GetRow(element);
            
            Debug.WriteLine("column = " + column + " row = " + row);

            bool isClicked = game.ClickSquare(row, column);
            if (isClicked)
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
