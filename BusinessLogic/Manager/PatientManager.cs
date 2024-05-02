using BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration _configuration;

        public PatientManager(IConfiguration configuration)
        {
            _patients = new List<Patient>();
            _configuration = configuration;

            leerPacientes();
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

        private string GenerateBloodtype()
        {
            string[] bloodtypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            Random rand = new Random();
            
            int randomNum = rand.Next(bloodtypes.Length);

            return bloodtypes[randomNum];
        }

        private void leerPacientes()
        {
            _patients.Clear();

            string patientsFile = _configuration.GetSection("PatientFilepath").Value;
            StreamReader lector = new StreamReader(patientsFile);

            while(!lector.EndOfStream)
            {
                string[] datosPaciente = lector.ReadLine().Split(",");
                Patient newPatient = new Patient()
                {
                    Name = datosPaciente[0],
                    Lastname = datosPaciente[1],
                    CI = int.Parse(datosPaciente[2]),
                    Bloodtype = datosPaciente[3]
                };

                _patients.Add(newPatient);
            }
            lector.Close();
        }

        private void escribirPacientes()
        {
            string patientsFile = _configuration.GetSection("PatientFilepath").Value;
            StreamWriter escritor = new StreamWriter(patientsFile);

            foreach(var patient in _patients)
            {
                string[] datosPaciente = { patient.Name, patient.Lastname, $"{patient.CI}", patient.Bloodtype };
                
                escritor.WriteLine(string.Join(",", datosPaciente));
            }
            escritor.Close();
        }
    }
}
