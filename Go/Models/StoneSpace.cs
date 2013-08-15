using Go.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.Models
{
    class StoneSpace
    {
        public Space Space { get; private set; }
        public StoneState State { get; private set; }

        private Board board;

        public StoneSpace(Board b, Space space)
        {
            State = StoneState.None;
            Space = space;

            board = b;
        }

        public int Row { get { return Space.Row; } }
        public int Column { get { return Space.Column; } }

        public StoneSpace Up()
        {
            // Return null if Upper neighbor is off the board
            return (Row-1 < 0) ? null : board.Grid[Row - 1, Column];
        }

        public StoneSpace Down()
        {
            // Return null if Lower neighbor is off the board
            return (Row+1 > (board.BoardSize-1)) ? null : board.Grid[Row + 1, Column];
        }

        public StoneSpace Left()
        {
            // Return null if Left neighbor is off the board
            return (Column-1 < 0) ? null : board.Grid[Row, Column - 1];
        }

        public StoneSpace Right()
        {
            // Return null if Right neighbor is off the board
            return (Column+1 > (board.BoardSize-1)) ? null : board.Grid[Row, Column + 1];
        }

        internal void SetState(StoneState stoneState)
        {
            State = stoneState;

            board.OnBoardChanged(this);
        }

        public static bool operator ==(StoneSpace space, StoneState state)
        {
            if (((object)space == null) || ((object)state == null))
            {
                return false;
            }

            // Return true if the fields match:
            return space.State == state;
        }

        public static bool operator !=(StoneSpace a, StoneState b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var item = obj as StoneSpace;

            if (item == null)
            {
                return false;
            }

            return this.State.Equals(item.State);
        }

        public override int GetHashCode()
        {
            return this.State.GetHashCode();
        }

        internal IEnumerable<StoneSpace> Neighbors()
        {
            List<StoneSpace> list = new List<StoneSpace>();
            list.Add(Up());
            list.Add(Down());
            list.Add(Left());
            list.Add(Right());
            // Remove null neighbors (aka edges)
            list.RemoveAll(neighbor => neighbor == null);
            return list.AsEnumerable();
        }

        public Chain GetChain()
        {
            return board.GetChain(this);
        }
    }
}
