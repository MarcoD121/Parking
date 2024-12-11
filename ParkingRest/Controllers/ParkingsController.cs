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
        [HttpGet("ActiveParkings")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ParkedVehicle>>> GetActiveParkings()
        {
                var ListOfActiveParkings = await _parkingRepo.GetActiveParkings();
                return ListOfActiveParkings.Count() == 0 ? NotFound() : Ok(ListOfActiveParkings);   
        }

        // GET: api/<ParkingsController>
        [HttpGet("EndedParkings")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<EndedParkedVehicle>>> GetEndedParkings()
        {
            var ListOfEndedParkings = await _parkingRepo.GetEndedParkings();
            return ListOfEndedParkings.Count() == 0 ? NotFound() : Ok(ListOfEndedParkings);
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
        public async Task<ActionResult> Post([FromBody] ParkingRequestDTO request)
        {
            APIClient client = new APIClient();
            var result = await client.LicensePlateInformationAsync(request.LicensePlate);
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
        [HttpPut("EndParking{licenseplate}")]
        public async Task<ActionResult> EndParking(string licenseplate, string datetime)
        {
            var result = await _parkingRepo.EndParking(licenseplate);
            if (result != null)
            {
            return Ok(result);
            }
            return NotFound();
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
