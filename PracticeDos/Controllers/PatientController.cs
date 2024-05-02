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
        private PatientManager _patienController;
        public PatientController()
        {
            _patienController = new PatientManager();
        }

        [HttpGet]
        public List<Patient> Get()
        {
            return _patienController.GetAll();
        }

        [HttpGet]
        [Route("{ci}")]
        public Patient Get(int ci)
        {
            return _patienController.GetById(ci);
        }

        [HttpPost]
        public void Post([FromBody] Patient patient)
        {
            _patienController.CreatePatient(patient);
        }

        [HttpPut("{ci}")]
        public void Put(int ci, [FromBody] Patient patient)
        {
            _patienController.UpdatePatient(ci, patient);
        }

        [HttpDelete("{ci}")]
        public void Delete(int ci)
        {
            _patienController.DeletePatient(ci);
        }
    }
}
