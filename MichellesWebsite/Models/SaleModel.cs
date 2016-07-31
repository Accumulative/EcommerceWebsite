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
        [Display(Name="SaleOnHold", ResourceType = typeof(ViewRes.SharedStrings))]
        Hold = 1,
        [Display(Name = "SaleDispatched", ResourceType = typeof(ViewRes.SharedStrings))]
        Dispatched = 2,
        [Display(Name = "SaleProcessing", ResourceType = typeof(ViewRes.SharedStrings))]
        Processing = 3,
        [Display(Name = "SaleFinished", ResourceType = typeof(ViewRes.SharedStrings))]
        Done = 4,
        [Display(Name = "SaleOrdered", ResourceType = typeof(ViewRes.SharedStrings))]
        Ordered = 5,
        [Display(Name = "SaleCancelled", ResourceType = typeof(ViewRes.SharedStrings))]
        Cancelled = 6,
        [Display(Name = "SalePayment", ResourceType = typeof(ViewRes.SharedStrings))]
        Ordering = 7,
        [Display(Name = "SaleCancelled", ResourceType = typeof(ViewRes.SharedStrings))]
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
        [Display(Name = "ProductQuantity", ResourceType = typeof(ViewRes.SharedStrings))]
        public int Quantity { get; set; }
    }
    public class SaleViewModel
    {
        public Guid SaleId { get; set; }
        public ApplicationUser User { get; set; }
        [Display(Name = "Status", ResourceType = typeof(ViewRes.SharedStrings))]
        public Status status { get; set; }
        [Display(Name = "SaleAmount", ResourceType = typeof(ViewRes.SharedStrings))]
        public decimal Amount { get; set; }
        [Display(Name = "Timestamp", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime TimeStamp { get; set; }
    }
    public class SaleDetailsModel
    {
        public SaleViewModel saleView { get; set; }
        public Address address { get; set; }
        public List<ApplicationCartItem> products { get; set; }
    }
}