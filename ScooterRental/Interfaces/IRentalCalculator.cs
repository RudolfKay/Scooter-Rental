namespace ScooterRental.Interfaces
{
    public interface IRentalCalculator
    {
        decimal GetFee(RentedScooter rentedScooter);
    }
}
