using System;

namespace GamesCatalogAPI.Services.Exceptions
{
    public class GameException : Exception
    {
        public GameException (string msg) : base(msg)
        {
        }
    }
}
