abstract class Doctor
{
    public string Name { get; set; }
    public int Experience { get; set; }
    public string Specialty { get; set; }

    public Doctor(string name, int experience, string specialty)
    {
        Name = name;
        Experience = experience;
        Specialty = specialty;
    }

    public abstract void PerformConsultation(Patient patient);  // Abstract method for consultation
}
