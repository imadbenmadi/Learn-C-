class Specialist : Doctor
{
    public Specialist(string name, int experience, string specialty) 
        : base(name, experience, specialty) {}

    public override void PerformConsultation(Patient patient)
    {
        Console.WriteLine($"Specialist {Name} is consulting with {patient.Name}.");
    }
}

