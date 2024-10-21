public delegate void AppointmentHandler(string message);

class Appointment
{
    public event AppointmentHandler OnAppointmentScheduled;

    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    public DateTime Date { get; set; }

    public Appointment(Doctor doctor, Patient patient, DateTime date)
    {
        Doctor = doctor;
        Patient = patient;
        Date = date;
    }

    public void Schedule()
    {
        OnAppointmentScheduled?.Invoke($"Appointment scheduled for {Patient.Name} with Dr. {Doctor.Name} on {Date}");
    }
}
