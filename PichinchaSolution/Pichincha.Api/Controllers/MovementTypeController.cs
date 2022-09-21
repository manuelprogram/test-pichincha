using Pichincha.Api.Controllers.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Models;

namespace Pichincha.Api.Controllers
{
    public class MovementTypeController : BaseController<MovementType, MovementTypeDto>
    {
        public MovementTypeController(IBaseDomain<MovementType, MovementTypeDto> baseDomain) : base(baseDomain) { }
    }
}