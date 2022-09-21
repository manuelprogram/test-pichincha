namespace Pichincha.Domain.Interfaces
{
    using Pichincha.Infrastructure.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseDomain<Entity, Dto> where Entity : class, IEntity
    {
        Task<Entity> GetByIdAsync(int id);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<IEnumerable<Entity>> InsertAsync(Dto dto);
        Task<bool> UpdateAsync(Dto dto);
        Task<bool> DeleteGetByIdAsync(int id);
    }
}
