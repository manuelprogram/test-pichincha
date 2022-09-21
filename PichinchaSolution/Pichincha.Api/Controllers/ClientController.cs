using Microsoft.AspNetCore.Mvc;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Models;

namespace Pichincha.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientDomain clientDomain;

        public ClientController(IClientDomain clientDomain)
        {
            this.clientDomain = clientDomain;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await clientDomain.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await clientDomain.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserDto dto)
        {
            return Ok(await clientDomain.InsertAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClientDto dto)
        {
            return Ok(await clientDomain.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGetById(int id)
        {
            return Ok(await clientDomain.DeleteGetByIdAsync(id));
        }
    }
}