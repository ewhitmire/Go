using Go.Common;
using Go.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Game : BindableBase
    {
        public Board GameBoard { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public List<Move> History { get; private set; }
        public float Komi { get; private set; }
        public int WhiteCaptured { get; private set; }
        public int BlackCaptured { get; private set; }
        public int NextMoveNumber { get; private set; }

        private Player WhoseTurn;

        public Game(int boardSize)
        {
            this.GameBoard = new Board(boardSize);
            this.Player1 = new Player(Color.BLACK);
            this.Player2 = new Player(Color.WHITE);
            this.History = new List<Move>();
            this.WhoseTurn = Player1;
            this.NextMoveNumber = 0;
        }

        internal void MakeMove(int r, int c)
        {
            try
            {


                if (IsKoRuleViolation(WhoseTurn, r, c))
                {
                }

                else
                {
                    GameBoard.PlacePieceByPlayer(WhoseTurn, r, c);

                    // Add to History
                    History.Add(new Move(WhoseTurn, new Tuple<int, int>(r, c), false, NextMoveNumber));
                    NextMoveNumber = History.Count();

                    // Switch turns
                    if (WhoseTurn == Player1)
                    {
                        WhoseTurn = Player2;
                    }
                    else
                    {
                        WhoseTurn = Player1;
                    }
                }
            }
            catch (InvalidMoveException e)
            {
                // NOOP;
            }
        }

        private bool IsKoRuleViolation(Player WhoseTurn, int r, int c)
        {
            if (NextMoveNumber < 2)
            {
                return false;
            }

            // todo: improve this really shitty code
            Board Copy = GetBoardBeforeMove(NextMoveNumber);
            Copy.PlacePieceByPlayer(WhoseTurn, r, c);
            Board PrevBoard = GetBoardBeforeMove(NextMoveNumber - 1);

            if (Enumerable.SequenceEqual<StoneSpace>(PrevBoard.Grid.Cast<StoneSpace>(), Copy.Grid.Cast<StoneSpace>()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void UndoMove()
        {
            Board PrevBoard = GetBoardBeforeMove(NextMoveNumber-1);
            for (int i = 0; i < GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < GameBoard.BoardSize; j++)
                {
                    if (PrevBoard.Grid[i, j] != GameBoard.Grid[i, j])
                    {
                        ResetPieceBind(PrevBoard.Grid[i, j].State, i, j);
                    }
                }
            }
            NextMoveNumber--;
        }

        private void ResetPieceBind(ViewModels.StoneState stoneState,int i,int j)
        {
            GameBoard.PlacePiece(stoneState, i, j);
        }

        internal Board GetBoardBeforeMove(int MoveNumber)
        {
            Board NewBoard = new Board(GameBoard.BoardSize);

            // Reenact "MoveNumber" (like 0, or 2, or 39) of the moves in history
            int i = 0;
            foreach (Move m in History)
            {
                if (i++ == MoveNumber)
                {
                    break;
                }
                NewBoard.PlacePieceByPlayer(m.WhoseTurn, m.CoordinatesMovedTo.Item1, m.CoordinatesMovedTo.Item2);
            }
            return NewBoard;
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
