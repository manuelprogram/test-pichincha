using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Infrastructure.Models
{
    public partial class MovementType : IEntity
    {
        public MovementType()
        {
            Movement = new HashSet<Movement>();
        }

        [Key]
        public int MovementTypeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [InverseProperty("MovementType")]
        public virtual ICollection<Movement> Movement { get; set; }
    }
}
