using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    class Player
    {
        public Color MyColor { get; private set; }
        public float Score { get; private set; }


        public Player(Color color)
        {
            this.MyColor = color;
        }
    }
}
