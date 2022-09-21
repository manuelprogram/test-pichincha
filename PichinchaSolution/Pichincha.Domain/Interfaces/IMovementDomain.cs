using Pichincha.Domain.Entities;
using Pichincha.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface IMovementDomain : IBaseDomain<Movement, MovementDto>
    {
        Task<bool> Transaction(TransactionDto transactionDto);
        Task<IEnumerable<Movement>> GetByAccountNumber(string accountNumber);
    }
}
