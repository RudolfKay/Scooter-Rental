using ScooterRental.Exceptions;
using System.Collections.Generic;
using System;

namespace ScooterRental.Interfaces
{
    public interface IRentalHistory
    {
        Dictionary<Scooter, Dictionary<int, decimal>> History { get; }
        
        void AddRent(Scooter scooter, DateTime starTime);

        RentedScooter UpdateRental(Scooter scooter, DateTime endTime);

        void AddIncome(Scooter scooter, int year, decimal income);

        Dictionary<Scooter, Dictionary<int, decimal>> GetHistory(int? year);

        List<RentedScooter> GetIncompleteRentals(int? year);
    }
}
