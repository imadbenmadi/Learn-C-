// See https://aka.ms/new-console-template for more information
class Program
{
    static async Task Main(string[] args)
    {
        // Creating doctors and patients
        Doctor generalDoctor = new GeneralDoctor("Dr. Smith", 10);
        Doctor cardiologist = new Specialist("Dr. Doe", 15, "Cardiology");
        
        Patient patient1 = new Patient("Alice", "Cold", "No significant history");
        Patient patient2 = new Patient("Bob", "Heart Pain", "Previous heart surgery");

        // Create Hospital instance
        Hospital<Doctor> hospital = new Hospital<Doctor>();
        
        hospital.AddDoctor(generalDoctor);
        hospital.AddDoctor(cardiologist);

        hospital.AddPatient(patient1);
        hospital.AddPatient(patient2);

        // Schedule Appointments
        hospital.ScheduleAppointment(generalDoctor, patient1, DateTime.Now.AddDays(1));
        hospital.ScheduleAppointment(cardiologist, patient2, DateTime.Now.AddDays(2));

        // Use LINQ to find a doctor by specialty
        Doctor foundDoctor = hospital.FindDoctor("Cardiology");
        if (foundDoctor != null)
        {
            Console.WriteLine($"Found Doctor: {foundDoctor.Name}, Specialty: {foundDoctor.Specialty}");
        }

        // Show all doctors
        hospital.ShowDoctors();

        // Billing example
        IBilling billing = new BillingSystem();
        billing.GenerateBill(patient2, 150.75);

        // Demonstrating async operation
        await PerformMedicalOperationAsync(patient2);
    }

    // Async example
    public static async Task PerformMedicalOperationAsync(Patient patient)
    {
        Console.WriteLine($"Starting operation for {patient.Name}...");
        await Task.Delay(2000);  // Simulate a 2-second operation
        Console.WriteLine($"Operation completed for {patient.Name}.");
    }
}
