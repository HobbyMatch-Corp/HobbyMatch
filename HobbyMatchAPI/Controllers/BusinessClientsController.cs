using HobbyMatch.BL.Services.BusinessClients;
using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessClientsController : ControllerBase
    {
        private readonly IBusinessClientService _businessClientService;

        public BusinessClientsController(IBusinessClientService businessClientService)
        {
            _businessClientService = businessClientService;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBusinessClientsAsync()
        {
            return Ok(await _businessClientService.GetBusinessClientsAsync());
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(int businessClientId)
        {
            var emailJwt = User.FindFirst("email")?.Value;

            var businessClient = await _businessClientService.GetBusinessClientByIdAsync(businessClientId);
            if (businessClient == null || string.IsNullOrEmpty(emailJwt) || businessClient.Email != emailJwt) return BadRequest();

            return Ok(businessClient);
        }

        [Authorize]
        [HttpPost("{businessClientId}")]
        public async Task<IActionResult> UpdateUserAsync(int businessClientId, [FromBody] BusinessClient businessClient)
        {
            var emailJwt = User.FindFirst("email")?.Value;

            var businessClientDb = await _businessClientService.GetBusinessClientByIdAsync(businessClientId);
            if (businessClientDb == null || string.IsNullOrEmpty(emailJwt) || businessClientDb.Email != emailJwt) return BadRequest();

            await _businessClientService.UpdateBusinessClientAsync(businessClientId, businessClientDb);

            return Ok();
        }
    }
}
