using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    class Move
    {
        public Player MyPlayer { get; private set; }
        public Tuple<int,int> CoordinatesMovedTo { get; private set; }
        public Boolean Pass { get; private set; }

    }
}
