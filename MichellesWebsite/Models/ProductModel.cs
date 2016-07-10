using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    public class ProductModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Product name")]
        public string name { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Product name (zh)")]
        public string zhName { get; set; }
        [Display(Name = "Description (zh)")]
        public string zhDescription { get; set; }
        [Display(Name = "Created")]
        public DateTime ts { get; set; }
        public string picture { get; set; }
        [MinValue(0)]
        public int stock { get; set; }
    }
    public class ProductViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Product name")]
        public string name { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Picture")]
        public string picture { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Quantity")]
        public uint quantity { get; set; }
    }
    public class StockTransaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        [Required]
        public int Amount { get; set; }
        public string Details { get; set; }
        public DateTime ts { get; set; }
    }
    public class MinValueAttribute:ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
            ErrorMessage = "Enter a value greater than or equal to " + _minValue;
        }
        public override bool IsValid(object value)
        {
            return (int)value >= _minValue;
        }
    }
}