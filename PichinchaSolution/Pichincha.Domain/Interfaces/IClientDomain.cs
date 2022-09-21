using Pichincha.Domain.Entities;
using Pichincha.Infrastructure.Models;

namespace Pichincha.Domain.Interfaces
{
    public interface IClientDomain : IBaseDomain<Client, ClientDto>
    {
        Task<IEnumerable<Client>> InsertAsync(UserDto userDto);
    }
}
