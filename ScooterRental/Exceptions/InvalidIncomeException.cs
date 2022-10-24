using System;

namespace ScooterRental.Exceptions
{
    public class InvalidIncomeException : Exception
    {
        public InvalidIncomeException():
            base($"Income amount is not valid"){}
    }
}
