using AutoMapper;
using Pichincha.Domain.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.DataAccess.Interfaces;
using Pichincha.Infrastructure.Models;
using Pichincha.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain
{
    public class ClientDomain : BaseDomain<Client, ClientDto>, IClientDomain
    {
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<Client> clientRepository;

        public ClientDomain(IMapper mapper,
            IRepository<Person> personRepository,
            IRepository<Client> clientRepository) : base(mapper, clientRepository)
        {
            this.personRepository = personRepository;
            this.clientRepository = clientRepository;
        }

        public override async Task<Client> GetByIdAsync(int id)
        {
            var clients = await clientRepository.GetAllAsync(x => x.ClientId == id, i => i.Person);
            ArgumentNullException.ThrowIfNull(clients);
            return clients.First();
        }

        public override async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await clientRepository.GetAllAsync(x => x.Person);
        }

        public override async Task<bool> UpdateAsync(ClientDto dto)
        {
            var entity = mapper.Map<Client>(dto);
            var oldEntity = await GetByIdAsync(dto.ClientId);
            if (oldEntity == null)
                return false;

            entity.PersonId = oldEntity.PersonId;
            clientRepository.Update(entity);
            var result = await clientRepository.SaveAsync();

            return result > 0;
        }

        public override async Task<bool> DeleteGetByIdAsync(int id)
        {
            var entity = await clientRepository.GetByIdAsync(id);
            if (entity != null)
                clientRepository.Delete(entity);

            var result = await clientRepository.SaveAsync();

            return result > 0;
        }

        public async Task<IEnumerable<Client>> InsertAsync(UserDto userDto)
        {
            var person = mapper.Map<Person>(userDto.Person);
            var client = mapper.Map<Client>(userDto.Client);
            var result = personRepository.Insert(person);
            _ = await personRepository.SaveAsync();
            client.PersonId = result.PersonId;
            _ = clientRepository.Insert(client);
            _ = await clientRepository.SaveAsync();

            return await clientRepository.GetAllAsync();
        }
    }
}
