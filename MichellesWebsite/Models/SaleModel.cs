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
        public Guid SaleId { get; set; }
        [StringLength(128)]
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public int AddressId { get; set; }
        public Status status { get; set; }
        public List<SaleProductModel> Products { get; set; }
        public DateTime ts { get; set; }

    }
    public enum Status
    {
        [Display(Name = "On hold")]
        Hold = 1,
        [Display(Name = "Dispatched")]
        Dispatched = 2,
        [Display(Name = "Processing")]
        Processing = 3,
        [Display(Name = "Done")]
        Done = 4,
        [Display(Name = "Ordered")]
        Ordered = 5,
        [Display(Name = "Cancelled")]
        Cancelled = 6,
        [Display(Name = "Processing payment")]
        Ordering = 7,
        [Display(Name = "Failed")]
        Failed = 8
    }
    public class SaleProductModel
    {
        [Key]
        public int SaleProductId { get; set; }
        [ForeignKey("SaleModel")]
        public Guid SaleId { get; set; }
        public virtual SaleModel SaleModel { get; set; }
        public int PriceId { get; set; }
        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        public virtual ProductModel ProductModel {get;set;}
        public uint Quantity { get; set; }
    }
    public class SaleViewModel
    {
        public Guid SaleId { get; set; }
        [Display(Name ="Full name")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public Status status { get; set; }
        public decimal Amount { get; set; }
        [Display(Name ="Time stamp")]
        public DateTime TimeStamp { get; set; }
    }
    public class SaleDetailsModel
    {
        public SaleViewModel saleView { get; set; }
        public Address address { get; set; }
        public List<ApplicationCartItem> products { get; set; }
    }
}