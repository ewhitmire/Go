using System;
namespace Go.ViewModels
{
    /// <summary>
    /// Defines values that indicate the visual state of a board space.
    /// </summary>
    public enum StoneState
    {
        None,
        PlayerOne,
        PlayerTwo
    }


    public static class StoneStateExtensions
    {
        public static StoneState Opponent(this StoneState thisState)
        {
            switch (thisState)
            {
                case StoneState.PlayerOne:
                    return StoneState.PlayerTwo;
                case StoneState.PlayerTwo:
                    return StoneState.PlayerOne;
                case StoneState.None:
                    throw new ArgumentException();
            }
            throw new ArgumentException();
        }
        public static bool IsOpponent(this StoneState thisState, StoneState otherState)
        {
            if (thisState == StoneState.None)
                return false;

            return otherState == thisState.Opponent();
        }
    }
}
