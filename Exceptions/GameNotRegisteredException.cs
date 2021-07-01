using System;

namespace ApiGamesCatalog.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException() : base("This game is not registered")
        {

        }
    }
}
