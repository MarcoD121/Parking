using Microsoft.AspNetCore.Mvc;
using ParkingLib.Models;
using ParkingLib.Services;
using ParkingRest.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly ParkingRepo _parkingRepo;
        public ParkingsController(ParkingRepo parkingRepo)
        {
            _parkingRepo = parkingRepo;
        }

        // GET: api/<ParkingsController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ParkedVehicle>>> Get()
        {
                var ListOfActiveParkings = await _parkingRepo.GetSqlList();
                return ListOfActiveParkings.Count() == 0 ? NotFound() : Ok(ListOfActiveParkings);   
        }

        // GET api/<ParkingsController>/5
        [HttpGet("{licenseplate}")]
        public async Task<ActionResult> Get(string licenseplate)
        {
            try
            {
                var result = await _parkingRepo.GetParkingById(licenseplate);
                var resultDTO = new ParkedVehicleDTO(result.LicensePlate, result.Make, result.Model, result.Color, result.NumberOfWheels, result.ActiveParked.TimeStarted);
                return Ok(resultDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ParkingsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string licensePlate)
        {
            APIClient client = new APIClient();
            var result = await client.LicensePlateInformationAsync(licensePlate);
            try
            {
                return Ok(_parkingRepo.CreateParking(result));
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // PUT api/<ParkingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ParkingsController>/5
        [HttpDelete("{licenseplate}")]
        public async Task<ActionResult> Delete(string licenseplate)
        {
            try
            {
                var result = await _parkingRepo.DeleteParking(licenseplate);
                return Ok(result);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
    }
}
