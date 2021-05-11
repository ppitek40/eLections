using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace eLections.Dtos
{
    public class PartyDto
    {
        public int Id { get; set; }
        [Required]
        public String ShortName { get; set; }
        public String FullName { get; set; }
        public bool IsCoalition { get; set; }
    }
}