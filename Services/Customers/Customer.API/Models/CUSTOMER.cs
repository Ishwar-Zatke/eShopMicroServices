using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer.API.Models
{
    public class CUSTOMER
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("contactNumber")]
        public string? ContactNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}
