using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    [NotMapped]
    public class ApplicationCartItem
    { 
        public int ProductId { get; set; }
        [Display(Name = "ProductQuantity", ResourceType = typeof(ViewRes.SharedStrings))]
        public int Quantity { get; set; }
        [Display(Name = "ProductsPrice", ResourceType = typeof(ViewRes.SharedStrings))]
        public decimal Price { get; set; }
        [Display(Name = "ProductsName", ResourceType = typeof(ViewRes.SharedStrings))]
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}