using Go.Common;
using Go.Models;
using Go.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Board : BindableBase
    {
        public class BoardChangedEventArgs : EventArgs
        {
            public BoardChangedEventArgs(Space space)
            {
                StoneSpace = space;
            }
            public Space StoneSpace { get; private set; }
        }
        public delegate void BoardChangedEventHandler(object sender, BoardChangedEventArgs state);
        public event BoardChangedEventHandler BoardChanged;

        public int BoardSize { get; private set; }

        public StoneState[,] Grid { get; private set; }

        public Board(int boardSize)
        {
            this.BoardSize = boardSize;
            Grid = new StoneState[boardSize, boardSize];
        }

        internal void PlacePiece(Player WhoseTurn, int x, int y)
        {
            if (Grid[x, y] == StoneState.None)
            {
                // TODO: Fix color -> id conversion
                Grid[x, y] = WhoseTurn.MyColor.ToStoneState();
                OnBoardChanged(new Space(x, y));
            }
            else
            {
                throw new InvalidMoveException();
            }
        }

        private void OnBoardChanged(Space space)
        {
            if (BoardChanged != null)
            {
                BoardChanged(this, new BoardChangedEventArgs(space));
            }
        }
    }
}
