using ScooterRental.Exceptions;
using ScooterRental.Interfaces;
using System;
using System.Linq;

namespace ScooterRental
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private readonly IScooterService _scooterService;
        private readonly IRentalHistory _rentalHistory;
        private readonly RentalCalculator _rentalCalculator;

        public RentalCompany(string companyName, IRentalHistory rentalHistory, IScooterService scooterService, RentalCalculator rentalCalculator)
        {
            Name = companyName;
            _scooterService = scooterService;
            _rentalHistory = rentalHistory;
            _rentalCalculator = rentalCalculator;
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);

            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            if (scooter == null)
            {
                throw new ScooterDoesNotExistException(id);
            }

            scooter.IsRented = true;
            _rentalHistory.AddRent(scooter, DateTime.UtcNow);
        }

        public decimal EndRent(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            var scoot = _scooterService.GetScooterById(id);

            if (scoot == null)
            {
                throw new ScooterDoesNotExistException(id);
            }

            var rental = _rentalHistory.UpdateRental(scoot, DateTime.Now);

            scoot.IsRented = false;
            decimal fee = _rentalCalculator.GetFee(rental);

            _rentalHistory.AddIncome(scoot, rental.EndTime.Value.Year, fee);

            return fee;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return _rentalCalculator.GetIncome(year, includeNotCompletedRentals, _rentalHistory);
        }
    }
}
