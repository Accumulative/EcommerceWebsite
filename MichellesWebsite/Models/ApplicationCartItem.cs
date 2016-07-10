using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    [NotMapped]
    public class ApplicationCartItem
    { 
        public int ProductId { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}