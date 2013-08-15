using Go.Common;
using Go.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.ViewModels
{
    class GameController : BindableBase
    {
        public GameController(Game game)
        {
            this.Game = game;
            game.GameBoard.BoardChanged += GameBoard_BoardChanged;
        }

        void GameBoard_BoardChanged(object sender, Board.BoardChangedEventArgs state)
        {
            OnPropertyChanged(String.Format("Item[{0},{1}]",
                    state.StoneSpace.Row, state.StoneSpace.Column));
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
        private RelayCommand<Space> _undoCommand;

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

        /// <summary>
        /// Gets the command for performing an undo.
        /// </summary>
        public RelayCommand<Space> UndoCommand
        {
            get
            {
                return _undoCommand ?? (_undoCommand =
                    new RelayCommand<Space>(p => Game.UndoMove()));
            }
        }

        private StoneState GetStoneState(String index)
        {
            var rc = index.Split(',');
            int row = Int32.Parse(rc[0]);
            int col = Int32.Parse(rc[1]);

            return Game.GameBoard.Grid[row, col].State;
        }

        public StoneState this[String index] { get { return GetStoneState(index); } }

    }
}
