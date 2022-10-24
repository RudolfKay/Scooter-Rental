namespace ScooterRental.Interfaces
{
    public interface IRentalCompany
    {
        string Name { get; }
        
        void StartRent(string id);
        
        decimal EndRent(string id);
        
        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}