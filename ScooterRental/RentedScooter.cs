using System;

namespace ScooterRental
{
    public class RentedScooter
    {
        public string Id { get; set; }
        public decimal PricePerMinute { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime? EndTime { get; set; }

        public RentedScooter(string id, decimal pricePerMin, DateTime start)
        {
            Id = id;
            PricePerMinute = pricePerMin;
            StarTime = start;
        }
    }
}
