using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_ADO_Net.Models
{
    public class ApplicationData
    {
        [Key]
        [Column("ID")]
        public long ID { get; set; }

        [Required]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("REGISTRATION_DT")]
        public DateTime RegistrationDate { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }
    }
}
