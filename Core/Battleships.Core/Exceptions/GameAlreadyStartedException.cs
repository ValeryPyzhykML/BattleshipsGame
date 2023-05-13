namespace Battleships.Core.Exceptions
{
    public class GameAlreadyStartedException : BattleshipsGameException
    {
        public GameAlreadyStartedException() : base("Cannot start the game because it is already in progress.")
        {
        }
    }
}
