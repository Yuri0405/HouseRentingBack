using ApartmentService.Models;
using ApartmentService.Serivces;
using Microsoft.AspNetCore.Mvc;
using WebAPIGateway.Models;

namespace ApartmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly ApartmentDataService _apartmentService;
        public ApartmentController(ApartmentDataService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartment([FromQuery] ApartmentQueryParams parameters)
        {
            var result = await _apartmentService.GetApartments(parameters);
            Console.WriteLine("Response: \n" + result);
            return Ok(result);
        }

        [HttpGet("owned")]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartmentsOwnedByUser(Guid id)
        {
            var result = await _apartmentService.GetApartmentPublishedByUser(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddApartment(Apartment apartment)
        {
            await _apartmentService.AddApartmentsAsync(apartment);

            return Ok("Apartment added");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateApartment(Apartment apartment)
        {
            await _apartmentService.UpdateApartmentsAsync(apartment);

            return Ok("Apartment updated");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteApartment(Guid id)
        {
            await _apartmentService.DeleteApartmentAsync(id);

            return Ok("Apartment updated");
        }
    }
}
