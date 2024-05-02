using BusinessLogic.Manager;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UPB.PracticeDos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientManager _patientManager;
        public PatientController(PatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        [HttpGet]
        public List<Patient> Get()
        {
            return _patientManager.GetAll();
        }

        [HttpGet]
        [Route("{ci}")]
        public Patient Get(int ci)
        {
            return _patientManager.GetById(ci);
        }

        [HttpPost]
        public void Post([FromBody] Patient patient)
        {
            _patientManager.CreatePatient(patient);
        }

        [HttpPut("{ci}")]
        public void Put(int ci, [FromBody] Patient patient)
        {
            _patientManager.UpdatePatient(ci, patient);
        }

        [HttpDelete("{ci}")]
        public void Delete(int ci)
        {
            _patientManager.DeletePatient(ci);
        }
    }
}
