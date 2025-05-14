using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.BL.Services.BusinessClients;
using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HobbyMatch.API.Controllers
{
    [Route("api/v1/[controller]")]
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
            var emailJwt = User.FindFirst(ClaimTypes.Email)?.Value;

            var businessClient = await _businessClientService.GetBusinessClientByIdAsync(businessClientId);
            if (businessClient == null || string.IsNullOrEmpty(emailJwt) || businessClient.Email != emailJwt) return BadRequest();

            return Ok(businessClient);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserAsync()
        {
            var emailJwt = User.FindFirst(ClaimTypes.Email)?.Value;
            if (emailJwt == null) return BadRequest("No Email found in Token");
            var businessClient = await _businessClientService.GetBusinessClientByEmailAsync(emailJwt);
            if (businessClient == null) return BadRequest();

            return Ok(businessClient);
        }

        [Authorize]
        [HttpPut("{businessClientId}")]
        public async Task<IActionResult> UpdateUserAsync(int businessClientId, [FromBody] UpdateBusinessClientDto businessClient)
        {
            var emailJwt = User.FindFirst(ClaimTypes.Email)?.Value;

            var businessClientDb = await _businessClientService.GetBusinessClientByIdAsync(businessClientId);
            if (businessClientDb == null || string.IsNullOrEmpty(emailJwt) || businessClientDb.Email != emailJwt) return BadRequest();

            await _businessClientService.UpdateBusinessClientAsync(businessClientId, businessClient);

            return Ok();
        }
    }
}
