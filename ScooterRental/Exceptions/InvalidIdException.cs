using System;

namespace ScooterRental.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException():
            base($"ID cannot be Null or Empty"){ }
    }
}
