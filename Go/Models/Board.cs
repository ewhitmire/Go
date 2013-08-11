using Go.Common;
using Go.Models;
using Go.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Board : BindableBase
    {
        public class BoardChangedEventArgs : EventArgs
        {
            public BoardChangedEventArgs(StoneSpace space)
            {
                StoneSpace = space;
            }
            public StoneSpace StoneSpace { get; private set; }
        }
        public delegate void BoardChangedEventHandler(object sender, BoardChangedEventArgs state);
        public event BoardChangedEventHandler BoardChanged;

        public int BoardSize { get; private set; }

        public StoneSpace[,] Grid { get; private set; }

        public Board(int boardSize)
        {
            this.BoardSize = boardSize;
            Grid = new StoneSpace[boardSize, boardSize];
            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    Grid[r, c] = new StoneSpace(this, new Space(r, c));
                }
            }
        }

        internal void PlacePiece(Player WhoseTurn, int r, int c)
        {
            StoneSpace stone = Grid[r, c];
            try
            {
                if (IsValidMove(WhoseTurn, stone))
                {
                    // TODO: Fix color -> id conversion
                    StoneState state = WhoseTurn.MyColor.ToStoneState();
                    stone.SetState(state);

                    ISet<Chain> captures = FindCaptures(stone, state);

                    // No suicides
                    if (stone.Neighbors().Count(s => s == state.Opponent()) == 4 && captures.Count == 0)
                    {
                        throw new InvalidMoveException();
                    }

                    foreach (Chain chain in captures)
                    {
                        Capture(chain);
                    }

                }
                else
                {
                    throw new InvalidMoveException();
                }
            }
            catch (InvalidMoveException e)
            {
                stone.SetState(StoneState.None);
                throw e;
            }
        }

        private ISet<Chain> FindCaptures(StoneSpace stone, StoneState state)
        {
            HashSet<Chain> captureChains = new HashSet<Chain>();
            foreach (StoneSpace neighbor in stone.Neighbors())
            {
                if (neighbor.State.IsOpponent(state))
                {
                    Chain c = neighbor.GetChain();
                    int liberties = c.GetLiberties();
                    Debug.WriteLine(liberties);
                    if (liberties == 0)
                    {
                        captureChains.Add(c);
                    }
                }
            }
            return captureChains;
        }

        private void Capture(Chain c)
        {
            foreach (StoneSpace stone in c)
            {
                stone.SetState(StoneState.None);
            }
        }


        internal Chain GetChain(StoneSpace stone)
        {
            Chain chain = new Chain();
            ISet<StoneSpace> unprocessed = new HashSet<StoneSpace>();

            unprocessed.Add(stone);

            while (unprocessed.Count > 0)
            {
                StoneSpace current = unprocessed.First();
                chain.Add(current);
                unprocessed.Remove(current);
                foreach (StoneSpace neighbor in current.Neighbors())
                {
                    if (chain.Contains(neighbor) || unprocessed.Contains(neighbor))
                    {
                        continue;
                    }
                    if (neighbor == stone.State)
                    {
                        unprocessed.Add(neighbor);
                    }
                }
            }
            return chain;
        }

        private bool IsValidMove(Player WhoseTurn, StoneSpace stone)
        {
            if (stone != StoneState.None)
            {
                return false;
            }

            // TODO: KO rule
            return true;
        }

        internal void OnBoardChanged(StoneSpace space)
        {
            if (BoardChanged != null)
            {
                BoardChanged(this, new BoardChangedEventArgs(space));
            }
        }
    }
}
