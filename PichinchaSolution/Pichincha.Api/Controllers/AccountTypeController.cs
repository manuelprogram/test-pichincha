using Pichincha.Api.Controllers.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Models;

namespace Pichincha.Api.Controllers
{
    public class AccountTypeController : BaseController<AccountType, AccountTypeDto>
    {
        public AccountTypeController(IBaseDomain<AccountType, AccountTypeDto> baseDomain) : base(baseDomain) { }
    }
}
