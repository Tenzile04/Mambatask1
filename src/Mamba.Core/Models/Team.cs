using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models
{
    public class Team : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public int ProfessionId { get; set; }
        public Profession? Profession { get; set; }
        public string LinkUrl {  get; set; }

        
    }
}
