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
                var ListOfActiveParkings = await _parkingRepo.GetAllActiveParkings();
                return ListOfActiveParkings.Count() == 0 ? NotFound() : Ok(ListOfActiveParkings);   
        }

        // GET api/<ParkingsController>/5
        [HttpGet("{licenseplate}")]
        public async Task<ActionResult> Get(string licenseplate)
        {
            try
            {
                var result = await _parkingRepo.GetParkingById(licenseplate);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ParkingsController>
        [HttpPost]
        public IActionResult Post([FromBody] ParkedVehicleDTO dto)
        {
            try
            {
                ParkedVehicle vehicle = ParkedVehicleConverter.Convert(dto);
                return Ok(_parkingRepo.CreateParking(vehicle));

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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
