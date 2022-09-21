
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Infrastructure.Models
{
    public partial class Person : IEntity
    {
        public Person()
        {
            Client = new HashSet<Client>();
        }

        [Key]
        public int PersonId { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [StringLength(200)]
        [Unicode(false)]
        public string Phone { get; set; } = null!;

        [InverseProperty("Person")]
        public virtual ICollection<Client> Client { get; set; }
    }
}
