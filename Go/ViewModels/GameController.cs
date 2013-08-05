using Go.Common;
using Go.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.ViewModels
{
    class GameController
    {

        public GameController(Game game)
        {
            this.Game = game;
        }

        /// <summary>
        /// Gets the number of rows in the current game.
        /// </summary>
        public int Rows { get { return Game.Rows; } }

        /// <summary>
        /// Gets the number of columns in the current game.
        /// </summary>
        public int Columns { get { return Game.Columns; } }

        public Game Game { get; private set; }

        private RelayCommand<Space> _moveCommand;

        /// <summary>
        /// Gets the command for performing a move.
        /// </summary>
        public RelayCommand<Space> MoveCommand
        {
            get
            {
                return _moveCommand ?? (_moveCommand =
                    new RelayCommand<Space>(p => Game.MakeMove(p.Row, p.Column)));
            }
        }
    }
}
