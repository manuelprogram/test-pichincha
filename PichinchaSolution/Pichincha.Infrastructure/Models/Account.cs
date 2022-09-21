using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Infrastructure.Models
{
    public partial class Account : IEntity
    {
        public Account()
        {
            Movement = new HashSet<Movement>();
        }

        [Key]
        public int AccountId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
        public int AccountTypeId { get; set; }
        public int Balance { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }

        [ForeignKey("AccountTypeId")]
        [InverseProperty("Account")]
        public virtual AccountType AccountType { get; set; } = null!;
        [ForeignKey("ClientId")]
        [InverseProperty("Account")]
        public virtual Client Client { get; set; } = null!;
        [InverseProperty("Account")]
        public virtual ICollection<Movement> Movement { get; set; }
    }
}
