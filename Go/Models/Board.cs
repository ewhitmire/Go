using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Board
    {
        private int boardSize;

        public Point[,] Grid { get; private set; }

        public Board(int boardSize)
        {
            this.boardSize = boardSize;
            Grid = new Point[boardSize, boardSize];
        }

        internal void PlacePiece(Player WhoseTurn, int x, int y)
        {
            if (Grid[x, y] == Point.EMPTY)
            {
                Grid[x, y] = (Point)WhoseTurn.MyColor;
            }
            else
            {
                throw new InvalidMoveException();
            }
        }
    }
}
