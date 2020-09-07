using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GCIT.Core.Entities
{
    [Table("UserCredentials")]
    public class UserCredentials
    {
        [Key]
        public int UserCredentialId { get; set; }
        [StringLength(150)]
        public string ApiKey { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
