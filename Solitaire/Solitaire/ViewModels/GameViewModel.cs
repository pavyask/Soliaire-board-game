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

        private ObservableCollection<GameButtonViewModel> _gameButtons = new ObservableCollection<GameButtonViewModel>();

        public GameViewModel(Game game)
        {
            _game = game;
            foreach (var cell in _game.Board.MarbleCells)
            {
                _gameButtons.Add(new GameButtonViewModel(cell, _game));
            }

        }

        public GameButtonViewModel GetGameButtonViewModelOnPos(Position pos)
        {
            return _gameButtons.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
        }

        [RelayCommand]
        private void Restart()
        {
            _game.Restart();
        }

        [RelayCommand]
        private void GoBack()
        {
            _game.MoveBack();
        }
    }
}
