using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    public class Contact
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Enquiry")]
        public int EnquiryId { get; set; }
        public virtual Enquiry Enquiry { get; set; }
        [Required]
        public string message { get; set; }
        public DateTime ts { get; set; }
        // true is yes, false is no
        public bool FromCustomer { get; set; }
    }
    public enum QueryType
    {
        [Display(Name="Product Query")]
        ProductQuery = 1,
        [Display(Name = "Order Query")]
        OrderQuery = 2,
        [Display(Name = "General Query")]
        GeneralQuery = 3
    }
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        [StringLength(128)]
        public string CustomerId { get; set; }
        [Required]
        public QueryType queryType { get; set; }
        public DateTime ts { get; set; }
    }
    public class ContactView
    {
        public int EnquiryId { get; set; }
        public QueryType queryType { get; set; }
        public string message { get; set; }
        public DateTime ts { get; set; }
    }
    public class EnquiryListView
    {
        public int EnquiryId { get; set; }
        public ApplicationUser user { get; set; }
        public QueryType queryType { get; set; }
        public string message { get; set; }
        public DateTime ts { get; set; }
    }
    public class ContactListView
    {
        public QueryType queryType { get; set; }
        public List<Contact> messages { get; set; }
    }
}