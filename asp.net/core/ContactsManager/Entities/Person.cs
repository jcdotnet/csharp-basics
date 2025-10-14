using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(40)] // nvarchar(40)
        public string? Name { get; set; }

        [StringLength(40)] // nvarchar(40)
        public string? Email { get; set; }

        [StringLength(200)] // nvarchar(200)
        public string? Address { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(10)] // nvarchar(10)
        public string? Gender { get; set; }

        public Guid? CountryId { get; set; } // FK
        
        public bool ReceiveNewsletters { get; set; }


    }
}
