using Go.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public enum Color
    {
        BLACK,
        WHITE
    }

    public static class ColorExtensions
    {
        public static StoneState ToStoneState(this Color value)
        {
            switch(value)
            {
                case Color.BLACK:
                    return StoneState.PlayerOne;
                case Color.WHITE:
                    return StoneState.PlayerTwo;
            }
            return StoneState.None;
        }
    }
}
