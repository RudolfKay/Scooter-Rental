using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ScooterRental.Exceptions;
using FluentAssertions;
using System;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalHistoryTests
    {
        private ScooterService _scooterService;
        private List<Scooter> _inventory;
        private RentalHistory _rentalHistory;
        private List<RentedScooter> _rentedInventory;
        private RentalCompany _rentalCompany;
        private RentalCalculator _rentalCalculator;

        [TestInitialize]
        public void Setup()
        {
            _inventory = new List<Scooter>();
            _rentedInventory = new List<RentedScooter>();
            _scooterService = new ScooterService(_inventory);
            _rentalCalculator = new RentalCalculator();

            for (int i = 1; i <= 5; i++)
            {
                _scooterService.AddScooter($"{i}", 0.2m);
            }
            
            _rentalHistory = new RentalHistory(_rentedInventory);
            _rentalCompany = new RentalCompany("Cheeki Breeki", _rentalHistory, _scooterService, _rentalCalculator);
        }

        [TestMethod]
        public void RentalHistory_InitializeRentalHistory_RentalHistoryCreated()
        {
            _rentalHistory.GetHistory(null).Count.Should().Be(0);
        }

        [TestMethod]
        public void AddIncome_AddIncomeWithNullScooter_ThrowsNullScooterException()
        {
            Scooter scooter = null;

            Action act = () =>
                _rentalHistory.AddIncome(scooter, 2010, 2.5m);

            act.Should().Throw<NullScooterException>()
                .WithMessage("Scooter is Null");
        }

        [TestMethod]
        public void AddIncome_AddInvalidIncomeAmount_ThrowsInvalidIncomeException()
        {
            Scooter scooter = _scooterService.GetScooterById("1");

            Action act = () =>
                _rentalHistory.AddIncome(scooter, 2010, -0.2m);

            act.Should().Throw<InvalidIncomeException>()
                .WithMessage("Income amount is not valid");
        }

        [TestMethod]
        public void GetHistory_AddValidScootersWithoutYear_HistoryIsCorrect()
        {
            _rentalCompany.StartRent("1");
            _rentalCompany.StartRent("2");
            _rentalCompany.StartRent("3");
            _rentalCompany.EndRent("1");
            _rentalCompany.EndRent("2");

            var history = _rentalHistory.GetHistory(null);

            history.Count.Should().Be(2);
        }

        [TestMethod]
        public void GetHistory_AddValidScootersWithYear_HistoryIsCorrect()
        {
            _rentalCompany.StartRent("1");
            _rentalCompany.StartRent("2");
            _rentalCompany.StartRent("3");
            _rentalCompany.EndRent("1");
            _rentalCompany.EndRent("2");
            _rentalCompany.EndRent("3");

            var history = _rentalHistory.GetHistory(2022);

            history.Count.Should().Be(3);
        }
    }
}
