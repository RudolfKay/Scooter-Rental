using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;
using FluentAssertions;
using System.Linq;
using System;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private IScooterService _scooterService;
        private List<Scooter> _inventory;

        [TestInitialize]
        public void Setup()
        {
            _inventory = new List<Scooter>();
            _scooterService = new ScooterService(_inventory);
        }

        [TestMethod]
        public void AddScooter_AddValidScooter_ScooterAdded()
        {
            _scooterService.AddScooter("1",0.2m);
            _inventory.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddScooter_AddScooterTwice_ThrowsDuplicateScooterException()
        {
            _scooterService.AddScooter("1",0.2m);

            Action act = () =>
                _scooterService.AddScooter("1",0.2m);

            act.Should().Throw<DuplicateScooterException>()
                .WithMessage("Scooter with ID 1 already exists");
        }

        [TestMethod]
        public void AddScooter_AddScooterWithNegativePrice_ThrowsNegativePriceException()
        {
            Action act = () =>
                _scooterService.AddScooter("1",-0.2m);

            act.Should().Throw<InvalidPriceException>()
                .WithMessage("Given price -0,2 is not valid");
        }

        [TestMethod]
        public void AddScooter_AddScooterNullOrEmptyID_ThrowsInvalidIdException()
        {
            Action act = () =>
                _scooterService.AddScooter("",0.2m);

            act.Should().Throw<InvalidIdException>()
                .WithMessage("ID cannot be Null or Empty");
        }

        [TestMethod]
        public void RemoveScooter_ScooterExists_ScooterIsRemoved()
        {
            _inventory.Add(new Scooter("1",0.2m));

            _scooterService.RemoveScooter("1");

            _inventory.Any(scooter => scooter.Id == "1").Should().BeFalse();
            _inventory.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveScooter_ScooterDoesNotExist_ThrowsScooterDoesNotExistException()
        {
            Action act = () =>
                _scooterService.RemoveScooter("1");

            act.Should().Throw<ScooterDoesNotExistException>()
                .WithMessage("Scooter with ID 1 does not exist.");
        }

        [TestMethod]
        public void RemoveScooter_NullOrEmptyIdGiven_ThrowsInvalidIdException()
        {
            Action act = () =>
                _scooterService.RemoveScooter("");

            act.Should().Throw<InvalidIdException>()
                .WithMessage("ID cannot be Null or Empty");
        }

        [TestMethod]
        public void GetScooters_ReturnScooterList()
        {
            _inventory.Add(new Scooter("1",0.2m));

            var scooters = _scooterService.GetScooters();
            scooters.Add(new Scooter("2",0.2m));

            var scooters2 = _scooterService.GetScooters();
            scooters2.Should().HaveCount(1);
        }

        [TestMethod]
        public void GetScooters_InventoryIsEmpty_ThrowsNoScootersFoundException()
        {
            _inventory.Clear();

            Action act = () =>
                _scooterService.GetScooters();

            act.Should().Throw<NoScootersFoundException>()
                .WithMessage("Inventory is Empty");
        }

        [TestMethod]
        public void GetScooterById_ReturnScooter()
        {
            _inventory.Add(new Scooter("1",0.2m));

            var scooter = _scooterService.GetScooterById("1");

            scooter.Id.Should().Be("1");
            scooter.PricePerMinute.Should().Be(0.2m);
        }

        [TestMethod]
        public void GetScooterById_NullOrEmptyIdGiven_ThrowsInvalidIdException()
        {
            Action act = () =>
                _scooterService.GetScooterById("");

            act.Should().Throw<InvalidIdException>()
                .WithMessage("ID cannot be Null or Empty");
        }

        [TestMethod]
        public void GetScooterById_ScooterDoesNotExist_ThrowsScooterDoesNotExistException()
        {
            Action act = () =>
                _scooterService.GetScooterById("1");

            act.Should().Throw<ScooterDoesNotExistException>()
                .WithMessage("Scooter with ID 1 does not exist.");
        }
    }
}
