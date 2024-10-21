class GeneralDoctor : Doctor
{
    public GeneralDoctor(string name, int experience) 
        : base(name, experience, "General Practitioner") {}

    public override void PerformConsultation(Patient patient)
    {
        Console.WriteLine($"General Doctor {Name} is consulting with {patient.Name}.");
    }
}
