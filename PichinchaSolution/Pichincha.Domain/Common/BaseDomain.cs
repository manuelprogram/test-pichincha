using AutoMapper;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.DataAccess.Interfaces;
using Pichincha.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pichincha.Domain.Common
{
    public class BaseDomain<Entity, Dto> : IBaseDomain<Entity, Dto> where Entity : class, IEntity
    {
        protected readonly IRepository<Entity> repositoryData;
        protected readonly IMapper mapper;

        public BaseDomain(IMapper mapper, IRepository<Entity> sqlData)
        {
            this.mapper = mapper;
            this.repositoryData = sqlData;
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await repositoryData.GetAllAsync();
        }

        public virtual Task<Entity> GetByIdAsync(int id)
        {
            return repositoryData.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<Entity>> InsertAsync(Dto dto)
        {
            var entity = mapper.Map<Entity>(dto);
            repositoryData.Insert(entity);
            await repositoryData.SaveAsync();

            return await repositoryData.GetAllAsync();
        }

        public virtual async Task<bool> UpdateAsync(Dto dto)
        {
            var entity = mapper.Map<Entity>(dto);
            repositoryData.Update(entity);
            var result = await repositoryData.SaveAsync();

            return result > 0;
        }

        public virtual async Task<bool> DeleteGetByIdAsync(int id)
        {
            var entity = await repositoryData.GetByIdAsync(id);
            if (entity != null)
                repositoryData.Delete(entity);

            var result = await repositoryData.SaveAsync();

            return result > 0;
        }
    }
}
