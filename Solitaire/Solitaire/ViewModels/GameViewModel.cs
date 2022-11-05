using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solitaire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitaire.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private readonly Game _game;

        public bool CheckWin => _game.CheckWin();

        public bool CheckLose => _game.CheckLose();

        public ObservableCollection<GameButtonViewModel> _gameButtons { get; } = new ObservableCollection<GameButtonViewModel>();

        public GameViewModel(Game game)
        {
            _game = game;

            foreach (var cell in _game.Board.MarbleCells)
            {
                _gameButtons.Add(new GameButtonViewModel(cell));
            }
        }

        public GameButtonViewModel GetGameButtonViewModelOnPos(Position pos)
        {
            return _gameButtons.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
        }

        [RelayCommand]
        private void Restart()
        {
            MessageBox.Show("Restart");
        }

        [RelayCommand]
        private void GoBack()
        {
            MessageBox.Show("Go Back");
        }
    }
}
