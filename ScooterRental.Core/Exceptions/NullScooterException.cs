using System;

namespace ScooterRental.Exceptions
{
    public class NullScooterException : Exception
    {
        public NullScooterException() :
            base($"Scooter is Null"){ }
    }
}
