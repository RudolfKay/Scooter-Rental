using System;

namespace ScooterRental.Exceptions
{
    public class YearNotValidException : Exception
    {
        public YearNotValidException() :
            base("Year is not in records"){ }
    }
}
