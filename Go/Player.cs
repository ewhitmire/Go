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

        private Color color;

        public Player(Color color)
        {
            this.color = color;
        }
    }
}
