using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pichincha.Infrastructure.Interfaces;

namespace Pichincha.Infrastructure.Models
{
    public partial class Movement : IEntity
    {
        [Key]
        public int MovementId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public int MovementTypeId { get; set; }
        public int Value { get; set; }
        public int Balance { get; set; }
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("Movement")]
        public virtual Account Account { get; set; } = null!;
        [ForeignKey("MovementTypeId")]
        [InverseProperty("Movement")]
        public virtual MovementType MovementType { get; set; } = null!;
    }
}
