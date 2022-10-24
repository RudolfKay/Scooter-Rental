using System.Collections.Generic;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;
using System.Linq;
using System;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooters;

        public ScooterService(List<Scooter> inventory)
        {
            _scooters = inventory;
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException(pricePerMinute);
            }

            if (_scooters.Any(scooter => scooter.Id == id))
            {
                throw new DuplicateScooterException(id);
            }

            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            var scooter = _scooters.FirstOrDefault(scooter => scooter.Id == id);

            if (scooter == null)
            {
                throw new ScooterDoesNotExistException(id);
            }

            _scooters.Remove(scooter);
        }

        public IList<Scooter> GetScooters()
        {
            if (_scooters.Count < 1)
            {
                throw new NoScootersFoundException();
            }

            return _scooters.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooters.FirstOrDefault(scooter => scooter.Id == scooterId);

            if (string.IsNullOrEmpty(scooterId))
            {
                throw new InvalidIdException();
            }

            if (scooter == null)
            {
                throw new ScooterDoesNotExistException(scooterId);
            }

            return scooter;
        }
    }
}
