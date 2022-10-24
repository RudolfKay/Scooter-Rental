using System;

namespace ScooterRental.Exceptions
{
    public class ScooterDoesNotExistException : Exception
    {
        public ScooterDoesNotExistException(string id):
            base($"Scooter with ID {id} does not exist."){}
    }
}
