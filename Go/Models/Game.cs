using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Game
    {
        public Board GameBoard { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public List<Move> History { get; private set; }
        public float Komi { get; private set; }
        public int WhiteCaptured { get; private set; }
        public int BlackCaptured { get; private set; }

        private Player WhoseTurn;

        public Game(int boardSize)
        {
            this.GameBoard = new Board(boardSize);
            this.Player1 = new Player(Color.BLACK);
            this.Player2 = new Player(Color.BLACK);
            this.History = new List<Move>();
            this.WhoseTurn = Player1;
        }

        internal void MakeMove(int x, int y)
        {
            GameBoard.PlacePiece(WhoseTurn,x,y);

            // Switch turns
            if(WhoseTurn == Player1)
            {
                WhoseTurn = Player2;
            }
            else
            {
                WhoseTurn = Player1;
            }
        }

        /// <summary>
        /// Gets the number of rows in the current game.
        /// </summary>
        public int Rows { get { return GameBoard.BoardSize; } }

        /// <summary>
        /// Gets the number of columns in the current game.
        /// </summary>
        public int Columns { get { return GameBoard.BoardSize; } }

    }

}
