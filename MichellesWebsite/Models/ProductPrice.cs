using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MichellesWebsite.Models
{
    public class ProductPrice
    {
        [Key]
        public int ID { get; set; }
        public int productID { get; set; }
        [Required]
        public DateTime dateFrom { get; set; }
        [Required]
        public DateTime dateTo { get; set; }
        [Required]
        public float price { get; set; }
    }
}