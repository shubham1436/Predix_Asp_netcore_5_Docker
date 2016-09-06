using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace AspnetCorePostgres.Models
{
    public class PartFamily
    {
        public int PartFamilyID { get; set; }

        [Required]
        public string PartFamilyName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int IsDeleted { get; set; }
    }
}
