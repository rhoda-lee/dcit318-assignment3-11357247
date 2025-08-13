using System;
using System.Collections.Generic;
using System.Linq;

public class HealthSystemApp
{
    private readonly Repository<Patient> _patientRepo = new();
    private readonly Repository<Prescription> _prescriptionRepo = new();
    private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new();

    public void SeedData()
    {
        // Patients
        _patientRepo.Add(new Patient(1, "Alice Smith", 30, "F"));
        _patientRepo.Add(new Patient(2, "Bob Johnson", 45, "M"));
        _patientRepo.Add(new Patient(3, "Celia Mensah", 22, "F"));

        // Prescriptions
        _prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin", DateTime.Now.AddDays(-10)));
        _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-5)));
        _prescriptionRepo.Add(new Prescription(3, 2, "Metformin", DateTime.Now.AddDays(-3)));
        _prescriptionRepo.Add(new Prescription(4, 3, "Paracetamol", DateTime.Now.AddDays(-1)));
        _prescriptionRepo.Add(new Prescription(5, 2, "Atorvastatin", DateTime.Now));
    }

    public void BuildPrescriptionMap()
    {
        _prescriptionMap.Clear();
        foreach (var p in _prescriptionRepo.GetAll())
        {
            if (!_prescriptionMap.ContainsKey(p.PatientId)) _prescriptionMap[p.PatientId] = new List<Prescription>();
            _prescriptionMap[p.PatientId].Add(p);
        }
    }

    public void PrintAllPatients()
    {
        Console.WriteLine("Patients:");
        foreach (var pat in _patientRepo.GetAll()) Console.WriteLine(pat);
    }

    public List<Prescription> GetPrescriptionsByPatientId(int patientId)
    {
        return _prescriptionMap.TryGetValue(patientId, out var list) ? new List<Prescription>(list) : new List<Prescription>();
    }

    public void PrintPrescriptionsForPatient(int id)
    {
        var pres = GetPrescriptionsByPatientId(id);
        if (!pres.Any()) Console.WriteLine($"No prescriptions found for patient {id}");
        else
        {
            Console.WriteLine($"Prescriptions for patient {id}:");
            foreach (var p in pres) Console.WriteLine(p);
        }
    }
}
