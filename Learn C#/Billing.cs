interface IBilling
{
    void GenerateBill(Patient patient, double amount);
}

class BillingSystem : IBilling
{
    public void GenerateBill(Patient patient, double amount)
    {
        Console.WriteLine($"Bill of {amount} generated for {patient.Name}.");
    }
}
