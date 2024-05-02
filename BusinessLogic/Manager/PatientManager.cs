using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public class PatientManager
    {
        private List<Patient> _patients;

        public PatientManager()
        {
            _patients = new List<Patient>();
        }

        public List<Patient> GetAll() { return _patients; }

        public Patient GetById(int ci)
        {  
            return _patients.Find(x => x.CI == ci); 
        }

        public void CreatePatient(Patient patient)
        {
            Patient createdPatient = new Patient()
            {
                Name = patient.Name,
                Lastname = patient.Lastname,
                CI = patient.CI,
                Bloodtype = GenerateBloodtype()
            };

            _patients.Add(createdPatient);
        }

        public void UpdatePatient(int ci, Patient patient)
        {
            Patient foundPatient = _patients.Find(x => x.CI == ci);

            foundPatient.Name = patient.Name;
            foundPatient.Lastname = patient.Lastname;
        }

        public void DeletePatient(int ci)
        {
            Patient patientToDelete = _patients.Find(x => x.CI == ci);
            _patients.Remove(patientToDelete);
        }

        private String GenerateBloodtype()
        {
            string[] bloodtypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            Random rand = new Random();
            
            int randomNum = rand.Next(bloodtypes.Length);

            return bloodtypes[randomNum];
        }
    }
}
