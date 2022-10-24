using System;

namespace ScooterRental.Exceptions
{
    public class NoScootersFoundException : Exception
    {
        public NoScootersFoundException() :
            base($"Inventory is Empty"){ }
    }
}
