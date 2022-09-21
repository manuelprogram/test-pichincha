namespace Pichincha.Domain
{
    using AutoMapper;
    using Pichincha.Domain.Entities;
    using Pichincha.Infrastructure.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<AccountType, AccountTypeDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Movement, MovementDto>().ReverseMap();
            CreateMap<MovementType, MovementTypeDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
