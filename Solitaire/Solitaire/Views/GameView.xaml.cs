using Solitaire.Models;
using Solitaire.ViewModels;
using System.Windows.Controls;

namespace Solitaire.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private const int BoardWidth = 7;
        private const int BoardHeight = 7;
        public GameViewModel GameViewModel => (GameViewModel)DataContext;

        public GameView()
        {
            DataContext = new GameViewModel(new Game(new Board1()));
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < BoardWidth; i++)
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < BoardHeight; i++)
                BoardGrid.RowDefinitions.Add(new RowDefinition());

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if ((x < 2 && y < 2) ||
                        (x < 2 && y > 4) ||
                        (x > 4 && y < 2) ||
                        (x > 4 && y > 4)
                        )
                        continue;

                    var pos = new Position(x, y);
                    var gameButtonViewModel = GameViewModel.GetGameButtonViewModelOnPos(pos);
                    GameButtonView button = new GameButtonView(gameButtonViewModel);
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    BoardGrid.Children.Add(button);
                }
            }
        }
    }
}
