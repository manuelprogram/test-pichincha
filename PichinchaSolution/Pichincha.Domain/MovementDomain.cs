using AutoMapper;
using Pichincha.Domain.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.DataAccess.Interfaces;
using Pichincha.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain
{
    public class MovementDomain : BaseDomain<Movement, MovementDto>, IMovementDomain
    {
        private readonly IRepository<Account> accountRepository;
        public MovementDomain(IMapper mapper,
            IRepository<Movement> sqlData,
            IRepository<Account> accountRepository) : base(mapper, sqlData)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Movement>> GetByAccountNumber(string accountNumber)
        {
            return await repositoryData.GetAllAsync(m => m.Account.Number.Equals(accountNumber));
        }

        public async Task<bool> Transaction(TransactionDto transactionDto)
        {
            ArgumentNullException.ThrowIfNull(transactionDto);

            var account = (await accountRepository.GetAllAsync(a => a.Number.Equals(transactionDto.AccountNumber), x => x.Client)).FirstOrDefault();

            if (account == null)
                throw new Exception("Account Not Found");

            if (!account.Client.Password.Equals(transactionDto.Password))
                throw new Exception("Wrong Account Password");

            if (transactionDto.MovementType == Common.Entities.MovementType.Credito)
            {
                account.Balance += transactionDto.Amount;
            }

            if (transactionDto.MovementType == Common.Entities.MovementType.Debito)
            {
                if (account.Balance - transactionDto.Amount < 0)
                    throw new Exception("Insufficient Balance");

                account.Balance -= transactionDto.Amount;
            }

            Movement movement = new()
            {
                Date = DateTime.Now,
                Value = transactionDto.Amount,
                MovementTypeId = (int)transactionDto.MovementType,
                AccountId = account.AccountId,
                Balance = account.Balance,
            };

            repositoryData.Insert(movement);
            _ = await repositoryData.SaveAsync();

            accountRepository.Update(account);
            _ = await accountRepository.SaveAsync();

            return true;
        }
    }
}
