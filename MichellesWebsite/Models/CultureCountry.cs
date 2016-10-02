using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    public class CultureCountry
    {
        [Key]
        public string CultureCountryId { get; set; }
        public Country country { get; set; }
        public string culture { get; set; }
    }
}