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
        [Display(Name = "DateFrom", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime dateFrom { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name = "DateTo", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime? dateTo { get; set; }
        [Display(Name = "Country", ResourceType = typeof(ViewRes.SharedStrings))]
        public Country country { get; set; }
        [Required]
        [Display(Name = "ProductsPrice")]
        public decimal price { get; set; }
    }
    public class ProductPriceView
    {
        public ProductPrice newProductPrice { get; set; }
        public List<ProductPrice> productPrices { get; set; }
    }
}