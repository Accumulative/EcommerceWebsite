using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    public class SaleModel
    {
        [Key]
        public int SaleId { get; set; }
        [StringLength(128)]
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int PriceId { get; set; }
        public float Amount { get; set; }
        public DateTime ts { get; set; }

    }
}