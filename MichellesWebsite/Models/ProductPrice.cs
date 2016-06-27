using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MichellesWebsite.Models
{
    public class ProductPrice
    {
        [Key]
        public int ID { get; set; }
        public int productID { get; set; }
        [Required]
        public DateTime dateFrom { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? dateTo { get; set; }
        [Required]
        public decimal price { get; set; }
    }
}