using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    public class ProductModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime ts { get; set; }
        public string picture { get; set; }
    }
}