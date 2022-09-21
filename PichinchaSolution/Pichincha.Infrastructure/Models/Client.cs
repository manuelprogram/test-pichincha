using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Infrastructure.Models
{
    public partial class Client : IEntity
    {
        public Client()
        {
            Account = new HashSet<Account>();
        }

        [Key]
        public int ClientId { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        public bool Status { get; set; }
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("Client")]
        public virtual Person Person { get; set; } = null!;
        [InverseProperty("Client")]
        public virtual ICollection<Account> Account { get; set; }
    }
}
