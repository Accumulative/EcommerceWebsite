using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    public class News
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string zhTitle { get; set; }
        public string zhDescription { get; set; }
        [Display(Name ="Time stamp")]
        public DateTime ts { get; set; }

    }
}