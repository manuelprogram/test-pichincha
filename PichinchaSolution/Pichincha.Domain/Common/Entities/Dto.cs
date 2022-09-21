namespace Pichincha.Domain.Entities
{
    using Pichincha.Domain.Common.Entities;
    using System;
    public record AccountDto(int AccountId, string Number, AccountType AccountTypeId, int Balance, bool Status, int ClientId);
    public record AccountTypeDto(int AccountTypeId, string Name);
    public record ClientDto(int ClientId, string Password, bool Status, int PersonId);
    public record UserDto(PersonDto Person, ClientDto Client);
    public record MovementDto(int MovementId, DateTime Date, int MovementTypeId, int Value, int Balance, int AccountId);
    public record TransactionDto(string AccountNumber, string Password, int Amount, MovementType MovementType);
    public record MovementTypeDto(int MovementTypeId, string Name);
    public record PersonDto(int PersonId, string Name, GenderType Gender, int Age, string Address, string Phone);
}