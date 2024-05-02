using BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UPB.BusinessLogic.Manager.Exceptions;

namespace BusinessLogic.Manager
{
    public class PatientManager
    {
        private List<Patient> _patients;
        private readonly IConfiguration _configuration;
        

        public PatientManager(IConfiguration configuration)
        {
            _patients = new List<Patient>();
            _configuration = configuration;

            leerPacientes();
        }

        public List<Patient> GetAll() { return _patients; }

        public Patient GetById(int ci)
        {
            Patient? foundPatient = _patients.Find(x => x.CI == ci);

            if (foundPatient == null)
            {
                PatientNotFoundException patientNotFoundException = new PatientNotFoundException();
                //Log.Information(patientNotFoundException.getError());
                throw patientNotFoundException;
            }

            return foundPatient; 
        }

        public void CreatePatient(Patient patient)
        {
            Patient? foundPatient = _patients.Find(x => x.CI == patient.CI);

            if (foundPatient != null)
            {
                PatientAlreadyExistsException patientNotFoundException = new PatientAlreadyExistsException();
                //Log.Information(patientNotFoundException.getError());
                throw patientNotFoundException;
            }

            Patient createdPatient = new Patient()
            {
                Name = patient.Name,
                Lastname = patient.Lastname,
                CI = patient.CI,
                Bloodtype = GenerateBloodtype()
            };

            _patients.Add(createdPatient);
            escribirPacientes();
        }

        public void UpdatePatient(int ci, Patient patient)
        {
            Patient? foundPatient = _patients.Find(x => x.CI == ci);

            if(foundPatient == null)
            {
                PatientNotFoundException patientNotFoundException = new PatientNotFoundException();
                //Log.Information(patientNotFoundException.getError());
                throw patientNotFoundException;
            }

            foundPatient.Name = patient.Name;
            foundPatient.Lastname = patient.Lastname;
            escribirPacientes();
        }

        public void DeletePatient(int ci)
        {
            Patient? patientToDelete = _patients.Find(x => x.CI == ci);

            if (patientToDelete == null)
            {
                PatientNotFoundException patientNotFoundException = new PatientNotFoundException();
                Log.Information(patientNotFoundException.getError());
                throw patientNotFoundException;
            }

            _patients.Remove(patientToDelete);
            escribirPacientes();
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

            string? patientsFile = _configuration.GetSection("PatientFilepath").Value;

            if(patientsFile ==null)
            {
                JsonSectionNotFoundException jsonSectionNotFoundException = new JsonSectionNotFoundException();
                

              
                throw jsonSectionNotFoundException;
            }

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
            string? patientsFile = _configuration.GetSection("PatientFilepath").Value;

            if (patientsFile == null)
            {
                JsonSectionNotFoundException jsonSectionNotFoundException = new JsonSectionNotFoundException();
                

                throw jsonSectionNotFoundException;
            }

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
