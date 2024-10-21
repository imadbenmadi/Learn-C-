class Hospital<TDoctor> where TDoctor : Doctor
{
    private List<TDoctor> doctors = new List<TDoctor>();
    private List<Patient> patients = new List<Patient>();
    private List<Appointment> appointments = new List<Appointment>();

    // Add doctors to hospital
    public void AddDoctor(TDoctor doctor)
    {
        doctors.Add(doctor);
    }

    // Add patients to hospital
    public void AddPatient(Patient patient)
    {
        patients.Add(patient);
    }

    // Create appointments and use events
    public void ScheduleAppointment(Doctor doctor, Patient patient, DateTime date)
    {
        var appointment = new Appointment(doctor, patient, date);
        appointment.OnAppointmentScheduled += Appointment_OnAppointmentScheduled;
        appointment.Schedule();
        appointments.Add(appointment);
    }

    private void Appointment_OnAppointmentScheduled(string message)
    {
        Console.WriteLine(message);  // Event handler for appointment scheduling
    }

    // Use LINQ to find a doctor
    public Doctor FindDoctor(string specialty)
    {
        return doctors.FirstOrDefault(doc => doc.Specialty == specialty);
    }

    // Print doctor list
    public void ShowDoctors()
    {
        foreach (var doctor in doctors)
        {
            Console.WriteLine($"Dr. {doctor.Name}, Specialty: {doctor.Specialty}");
        }
    }
}
