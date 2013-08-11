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
            return board.Grid[Row - 1, Column];
        }

        public StoneSpace Down()
        {
            return board.Grid[Row + 1, Column];
        }

        public StoneSpace Left()
        {
            return board.Grid[Row, Column - 1];
        }

        public StoneSpace Right()
        {
            return board.Grid[Row, Column + 1];
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


        internal IEnumerable<StoneSpace> Neighbors()
        {
            List<StoneSpace> list = new List<StoneSpace>();
            list.Add(Up());
            list.Add(Down());
            list.Add(Left());
            list.Add(Right());
            return list.AsEnumerable();
        }

        public Chain GetChain()
        {
            return board.GetChain(this);
        }
    }
}
