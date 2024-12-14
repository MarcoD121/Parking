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
        /// <summary>
        /// An instance field of the class parkingrepo
        /// </summary>
        private readonly IParkingRepo _parkingRepo;

        /// <summary>
        /// Constructor creating an instance of the Controller and using dependency injection to get an instance of the parking repo class
        /// </summary>
        /// <param name="parkingRepo">Class parkingrepo</param>
        public ParkingsController(IParkingRepo parkingRepo)
        {
            _parkingRepo = parkingRepo;
        }

        /// <summary>
        /// GET method for retrieving a list of currently parked vehicles
        /// </summary>
        /// <returns>A list of currently parked vehicles or a status code notfound</returns>
        [HttpGet("ActiveParkings")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ParkedVehicle>>> GetActiveParkings()
        {
                var ListOfActiveParkings = await _parkingRepo.GetActiveParkings();
                return ListOfActiveParkings.Count() == 0 ? NotFound() : Ok(ListOfActiveParkings);   
        }

        /// <summary>
        /// Get method for retrieving a list of ended parked vehicles
        /// </summary>
        /// <returns>A list of ended parked vehicles or a notfound status code</returns>
        [HttpGet("EndedParkings")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<EndedParkedVehicle>>> GetEndedParkings()
        {
            var ListOfEndedParkings = await _parkingRepo.GetEndedParkings();
            return ListOfEndedParkings.Count() == 0 ? NotFound() : Ok(ListOfEndedParkings);
        }

        /// <summary>
        /// Get method for retrieving a single currently parked vehicle
        /// </summary>
        /// <param name="licenseplate">Unique ID retrieved from the URI</param>
        /// <returns>A parked vehicle or a bad request status code with a keynotfound exception</returns>
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

        /// <summary>
        /// Post method for creating a parking for a vehicle
        /// </summary>
        /// <param name="request">A DTO containing the licenseplate of the vehicle</param>
        /// <returns>The parked vehicle object</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParkingRequestDTO request)
        {
            try
            {
                APIClient client = new APIClient();
                var result = await client.LicensePlateInformationAsync(request.LicensePlate);
                if (result != null) 
                {
                    var carparked = _parkingRepo.CreateParking(result);
                    return Ok(carparked);
                }
                return BadRequest();
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Put method for ending af parked vehicle
        /// </summary>
        /// <param name="licenseplate">Unique Id retrieved from the URI</param>
        /// <returns>The ended parked vehicle</returns>
        [HttpPut("{licenseplate}")]
        public async Task<ActionResult> EndParking(string licenseplate)
        {
            try
            {
            var result = await _parkingRepo.EndParking(licenseplate);
            if (result != null)
            {
            return Ok(result);
            }
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            return NotFound();
        }

        /// <summary>
        /// Delete method for deleting a parked vehicle
        /// </summary>
        /// <param name="licenseplate">Unique Id retrieved from the URI</param>
        /// <returns>The deleted parked vehicle</returns>
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
