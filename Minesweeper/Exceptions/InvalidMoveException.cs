using System;

namespace Minesweeper.Exceptions
{
    public class InvalidMoveException : GameException
    {
        public InvalidMoveException()
        {
        }

        public InvalidMoveException(string message) : base(message)
        {
        }

        public InvalidMoveException(string message, Exception inner) :base(message, inner)
        {
        }
    }
}