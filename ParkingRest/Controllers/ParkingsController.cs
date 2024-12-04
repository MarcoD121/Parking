using Microsoft.AspNetCore.Mvc;
using ParkingLib.Models;
using ParkingLib.Services;

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
            try
            {
                var ListOfActiveParkings = await _parkingRepo.GetAllActiveParkings();
                return Ok(ListOfActiveParkings);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
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
        public void Post([FromBody] string value)
        {
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
