using Pichincha.Api.Controllers.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Models;

namespace Pichincha.Api.Controllers
{
    public class AccountController : BaseController<Account, AccountDto>
    {
        public AccountController(IBaseDomain<Account, AccountDto> baseDomain) : base(baseDomain) { }
    }
}
