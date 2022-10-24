using ScooterRental.Interfaces;
using System.Linq;
using System;

namespace ScooterRental
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal GetFee(RentedScooter rentedScooter)
        {
            DateTime startRentTime = rentedScooter.StarTime;
            DateTime endRentTime = (DateTime)rentedScooter.EndTime;
            TimeSpan timeSpanRented = endRentTime - startRentTime;

            decimal pricePerMin = rentedScooter.PricePerMinute;
            decimal amount = 0.0m;
            decimal maxDailyCharge = 20.0m;
            
            int fullDaysRented = (int)timeSpanRented.TotalDays;
            int hourDifference = (int)timeSpanRented.Hours;
            int minuteDifference = (int)timeSpanRented.Minutes;
            int startHours = startRentTime.Hour;
            int endHours = endRentTime.Hour;
            int endMinutes = endRentTime.Minute;

            if (fullDaysRented > 0)
            {
                amount += (maxDailyCharge * fullDaysRented);

                if (startHours > endHours)
                {
                    if ((60 * endHours + endMinutes) * pricePerMin < maxDailyCharge)
                    {
                        amount += (60 * endHours + endMinutes) * pricePerMin;
                    }

                    if ((60 * endHours + endMinutes) * pricePerMin > maxDailyCharge)
                    {
                        amount += maxDailyCharge;
                    }
                }

                else if (startHours <= endHours)
                {
                    if ((60 * hourDifference + minuteDifference) * pricePerMin < maxDailyCharge)
                    {
                        amount += (60 * hourDifference + minuteDifference) * pricePerMin;
                    }

                    if ((60 * hourDifference + minuteDifference) * pricePerMin > maxDailyCharge)
                    {
                        amount += maxDailyCharge;
                    }
                }
            }

            if (fullDaysRented == 0)
            {
                if (startHours > endHours)
                {
                    if ((60 * hourDifference + minuteDifference) * pricePerMin < maxDailyCharge)
                    {
                        amount += (60 * hourDifference + minuteDifference) * pricePerMin;
                    }

                    if ((60 * hourDifference + minuteDifference) * pricePerMin > maxDailyCharge)
                    {
                        amount += maxDailyCharge;
                    }
                }

                else if (startHours <= endHours)
                {
                    if ((60 * hourDifference + minuteDifference) * pricePerMin < maxDailyCharge)
                    {
                        amount += (60 * hourDifference + minuteDifference) * pricePerMin;
                    }

                    if ((60 * hourDifference + minuteDifference) * pricePerMin > maxDailyCharge)
                    {
                        amount += maxDailyCharge;
                    }
                }
            }
            
            return amount;
        }

        public decimal GetIncome(int? year, bool includeNotCompletedRentals, IRentalHistory rentalHistory)
        {
            if (year.HasValue)
            {
                if (includeNotCompletedRentals && year.Value != DateTime.Today.Year)
                {
                    throw new InvalidOperationException();
                }

                var items = rentalHistory.GetHistory(year);
                var profit = items.Select(i => i.Value[year.Value]).Sum();

                if (includeNotCompletedRentals)
                {
                    var incompleteRentals = rentalHistory.GetIncompleteRentals(year);

                    foreach (var rentedScooter in incompleteRentals)
                    {
                        rentedScooter.EndTime = DateTime.UtcNow;
                        profit += GetFee(rentedScooter);
                    }
                }

                return profit;
            }

            if (!includeNotCompletedRentals)
            {
                var items = rentalHistory.GetHistory(null);
                var profit = items.Select(i => i.Value.Values.Sum()).Sum();

                return profit;
            }
            else
            {
                var items = rentalHistory.GetHistory(null);
                var profit = items.Select(i => i.Value.Values.Sum()).Sum();
                var incompleteRentals = rentalHistory.GetIncompleteRentals(null);

                foreach (var rentedScooter in incompleteRentals)
                {
                    rentedScooter.EndTime = DateTime.UtcNow;
                    profit += GetFee(rentedScooter);
                }

                return profit;
            }
        }
    }
}
