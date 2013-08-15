using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    class Move
    {
        public Player WhoseTurn { get; private set; }
        public Tuple<int, int> CoordinatesMovedTo { get; private set; }
        public Boolean Pass { get; private set; }
        // The number the move is in the game. 0 for the first black move, 1 as the first white move, and so on
        public int Number { get; private set; }

        public Move(Player WhoseTurn, Tuple<int, int> CoordinatesMovedTo, bool Pass, int Number)
        {
            this.WhoseTurn = WhoseTurn;
            this.CoordinatesMovedTo = CoordinatesMovedTo;
            this.Pass = Pass;
            this.Number = Number;
        }


    }
}
