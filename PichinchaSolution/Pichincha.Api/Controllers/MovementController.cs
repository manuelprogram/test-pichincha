using Microsoft.AspNetCore.Mvc;
using Pichincha.Api.Controllers.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Models;

namespace Pichincha.Api.Controllers
{
    public class MovementController : BaseController<Movement, MovementDto>
    {
        private readonly IMovementDomain movementDomain;
        public MovementController(IMovementDomain movementDomain) : base(movementDomain)
        {
            this.movementDomain = movementDomain;
        }

        [HttpPost]
        public override async Task<IActionResult> Insert([FromBody] MovementDto dto)
        {
            var result = await Task.FromResult("Service disable!");
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Transaction([FromBody] TransactionDto dto)
        {
            var result = await movementDomain.Transaction(dto);
            return Ok(result);
        }

        [HttpGet("{number}")]
        public virtual async Task<IActionResult> GetByAccountNumber(string number)
        {
            return Ok(await movementDomain.GetByAccountNumber(number));
        }

    }
}