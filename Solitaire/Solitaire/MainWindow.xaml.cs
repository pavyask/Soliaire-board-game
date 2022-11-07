using Solitaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Solitaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BoardWidth = 7;
        private const int BoardHeight = 7;

        public List<MarbleCell> MarbleCells { get; private set; }

        public List<Move> Moves { get; private set; } = new List<Move>();

        public MarbleCell? CurrentMarble { get; private set; }

        public int MarbleCounter { get; set; } = 32;

        public MainWindow()
        {
            DataContext = this;
            MarbleCells = new List<MarbleCell>();
            Moves = new List<Move>();
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
                        (x > 4 && y > 4))
                        continue;
                    else
                    {
                        Style style = new Style(typeof(Border));
                        style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(100)));
                        var marbleCell = new MarbleCell(x, y, false);
                        marbleCell.Resources.Add(typeof(Border), style);
                        if (x == 3 && y == 3)
                            marbleCell.IsEmpty = true;

                        marbleCell.Click += MoveAttempt;
                        Grid.SetColumn(marbleCell, x);
                        Grid.SetRow(marbleCell, y);
                        BoardGrid.Children.Add(marbleCell);
                        MarbleCells.Add(marbleCell);

                    }
                }
            }
        }

        public void MoveAttempt(object sender, RoutedEventArgs e)
        {
            if (CheckWin() || CheckLose())
                return;
            var cellSender = (MarbleCell)sender;
            Position pos = cellSender.Position;
            var marbleCell = GetMarbleCellOnPos(pos);

            if (marbleCell.IsEmpty && CurrentMarble == null)
                return;

            else if (!marbleCell.IsEmpty)
            {
                DeselectCurrentMarble();
                CurrentMarble = marbleCell;
                CurrentMarble.IsSelected = true;
            }

            else if (marbleCell.IsEmpty && CurrentMarble != null)
            {
                if (IsMoveValid(CurrentMarble, marbleCell))
                {
                    MoveMarble(marbleCell);
                    DeselectCurrentMarble();
                }
            }

            if (CheckWin())
                MessageBox.Show("You WON!", "Win Message");
            else if (CheckLose())
                MessageBox.Show("You LOST!", "Lose Message");
        }

        public bool CheckWin() => MarbleCounter == 1;

        public bool CheckLose()
        {
            foreach (var cell in MarbleCells)
            {
                if (ExistValidMove(cell)) return false;
            }
            return true;
        }

        public bool ExistValidMove(MarbleCell cell)
        {
            if (cell.IsEmpty == true)
                return false;

            var x = cell.Position.X;
            var y = cell.Position.Y;

            List<MarbleCell?> cellsToCheck = new List<MarbleCell?>
            {
                GetMarbleCellOnPos(x, y - 2),
                GetMarbleCellOnPos(x, y + 2),
                GetMarbleCellOnPos(x - 2, y),
                GetMarbleCellOnPos(x + 2, y)
            };

            foreach (var cellToCheck in cellsToCheck)
            {
                if (IsMoveValid(cell, cellToCheck)) return true;
            }

            return false;
        }

        public bool IsMoveValid(MarbleCell? marble, MarbleCell? destCell)
        {
            if (marble == null || destCell == null ||
                marble.IsEmpty || !destCell.IsEmpty)
                return false;

            var destX = destCell.Position.X;
            var destY = destCell.Position.Y;

            var midX = (marble.Position.X + destX) / 2;
            var midY = (marble.Position.Y + destY) / 2;

            if (((Math.Abs(marble.Position.Y - destY) == 2 &&
                  marble.Position.X == destX)
                 ||
                 (Math.Abs(marble.Position.X - destX) == 2 &&
                  marble.Position.Y == destY))
                &&
                (!IsCellEmpty(midX, midY)))
                return true;

            else return false;
        }

        public void MoveMarble(MarbleCell? destCell)
        {
            if (CurrentMarble == null || destCell == null ||
                CurrentMarble.IsEmpty || !destCell.IsEmpty)
                return;

            var destX = destCell.Position.X;
            var destY = destCell.Position.Y;
            var midX = (CurrentMarble.Position.X + destX) / 2;
            var midY = (CurrentMarble.Position.Y + destY) / 2;

            var removedMarble = RemoveMarbleOnPos(midX, midY);
            removedMarble.IsEmpty = true;
            Moves.Add(new Move(CurrentMarble, destCell, removedMarble));

            CurrentMarble.IsEmpty = true;
            destCell.IsEmpty = false;
            DeselectCurrentMarble();
        }

        public void MoveBack(object sender, RoutedEventArgs e)
        {
            if (Moves.Count == 0)
                return;

            var lastMove = Moves.Last();
            lastMove.ToCell.IsEmpty = true;
            lastMove.RemovedMarble.IsEmpty = false;
            lastMove.FromCell.IsEmpty = false;
            MarbleCounter++;
            counter.Content = MarbleCounter;
            Moves.Remove(lastMove);
            DeselectCurrentMarble();
        }

        public MarbleCell? GetMarbleCellOnPos(int x, int y) =>
            MarbleCells.Find(cell => cell.Position.X == x && cell.Position.Y == y);

        public MarbleCell? GetMarbleCellOnPos(Position position) => MarbleCells.Find(cell =>
            cell.Position.X == position.X && cell.Position.Y == position.Y);

        public MarbleCell? RemoveMarbleOnPos(int x, int y)
        {
            var marbelCell = GetMarbleCellOnPos(x, y);
            if (marbelCell == null || marbelCell.IsEmpty)
                return null;
            else
            {
                marbelCell.IsEmpty = false;
                MarbleCounter--;
                counter.Content = MarbleCounter;
                return marbelCell;
            }
        }

        public bool IsCellEmpty(int x, int y) => GetMarbleCellOnPos(x, y).IsEmpty;

        public void DeselectCurrentMarble()
        {
            if (CurrentMarble != null)
            {
                CurrentMarble.IsSelected = false;
                CurrentMarble = null;
            }
        }

        public void Restart(object sender, RoutedEventArgs e)
        {
            DeselectCurrentMarble();
            Moves.Clear();
            ResetBoard();
        }


        public void ResetBoard()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if ((x < 2 && y < 2) ||
                        (x < 2 && y > 4) ||
                        (x > 4 && y < 2) ||
                        (x > 4 && y > 4))
                        continue;
                    else if (x == 3 && y == 3)
                        MarbleCells.Find(cell => cell.Position.X == x && cell.Position.Y == y).IsEmpty = true;
                    else
                        MarbleCells.Find(cell => cell.Position.X == x && cell.Position.Y == y).IsEmpty = false;
                }
            }
            var test = MarbleCells.Find(cell => cell.Position.X == 3 && cell.Position.Y == 3);

            MarbleCounter = 32;
            counter.Content = MarbleCounter;
        }
    }
}
