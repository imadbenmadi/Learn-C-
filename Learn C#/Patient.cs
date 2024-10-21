class Patient
{
    public string Name { get; set; }
    public string Disease { get; set; }
    private string MedicalHistory { get; set; }  // Private encapsulated data

    public Patient(string name, string disease, string history)
    {
        Name = name;
        Disease = disease;
        MedicalHistory = history;
    }

    public void ShowMedicalHistory()
    {
        Console.WriteLine($"Medical History of {Name}: {MedicalHistory}");
    }
}
