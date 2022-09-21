using Microsoft.AspNetCore.Mvc;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Api.Controllers.Common
{

    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class BaseController<Entity, Dto> : ControllerBase where Entity : class, IEntity
    {
        protected readonly IBaseDomain<Entity, Dto> baseDomain;

        public BaseController(IBaseDomain<Entity, Dto> baseDomain)
        {
            this.baseDomain = baseDomain;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            return Ok(await baseDomain.GetByIdAsync(id));
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            return Ok(await baseDomain.GetAllAsync());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Insert([FromBody] Dto dto)
        {
            return Ok(await baseDomain.InsertAsync(dto));
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] Dto dto)
        {
            return Ok(await baseDomain.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteGetById(int id)
        {
            return Ok(await baseDomain.DeleteGetByIdAsync(id));
        }
    }
}