using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Infrastructure.Models
{
    public partial class AccountType: IEntity
    {
        public AccountType()
        {
            Account = new HashSet<Account>();
        }

        [Key]
        public int AccountTypeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [InverseProperty("AccountType")]
        public virtual ICollection<Account> Account { get; set; }
    }
}
