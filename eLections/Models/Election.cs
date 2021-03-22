using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class Election
    {
        public int ID { get; set; }
        [Display(Name = "Głosy przeliczone")]
        public Boolean IsCalculating { get; set; }
        [Display(Name = "Wybory Zakończone")]
        public Boolean IsClosed { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data zakończenia Wyborów")]
        public DateTime EndOfElections { get; set; }
    }
}