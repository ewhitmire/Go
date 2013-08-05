using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Board
    {
        public int BoardSize { get; private set; }

        public Point[,] Grid { get; private set; }

        public Board(int boardSize)
        {
            this.BoardSize = boardSize;
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
