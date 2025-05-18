using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.BL.Services.Hobbies;
using HobbyMatch.BL.Services.Venues;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers
{
    [ApiController]
    [Route("/api/v1/hobbies")]
    public class HobbiesController : Controller
    {
        private readonly IHobbyService _hobbyService;

        public HobbiesController(IHobbyService hobbyService)
        {
            _hobbyService = hobbyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHobbies()
        {
            var hobbies = await _hobbyService.GetHobbiesAsync();
            return Ok(hobbies.Select(h => h.ToDto()).ToList());
        }

    }
}
