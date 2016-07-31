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
        [Required(ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "EnquiryError")]
        [Display(Name = "Message", ResourceType = typeof(ViewRes.SharedStrings))]
        public string message { get; set; }
        [Display(Name = "Timestamp", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime ts { get; set; }
        // true is yes, false is no
        public bool FromCustomer { get; set; }
    }
    public enum QueryType
    {
        [Display(Name = "ProductQuery", ResourceType = typeof(ViewRes.SharedStrings))]
        ProductQuery = 1,
        [Display(Name = "OrderQuery", ResourceType = typeof(ViewRes.SharedStrings))]
        OrderQuery = 2,
        [Display(Name = "GeneralQuery", ResourceType = typeof(ViewRes.SharedStrings))]
        GeneralQuery = 3
    }
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        [StringLength(128)]
        public string CustomerId { get; set; }
        [Required]
        [Display(Name = "QueryType", ResourceType = typeof(ViewRes.SharedStrings))]
        public QueryType queryType { get; set; }
        [Display(Name = "Timestamp", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime ts { get; set; }
    }
    public class ContactView
    {
        public int EnquiryId { get; set; }
        [Display(Name = "QueryType", ResourceType = typeof(ViewRes.SharedStrings))]
        public QueryType queryType { get; set; }
        [Required(ErrorMessageResourceType = typeof(ViewRes.SharedStrings),
                      ErrorMessageResourceName = "EnquiryError")]
        [Display(Name = "Message", ResourceType = typeof(ViewRes.SharedStrings))]
        public string message { get; set; }
        [Display(Name = "Timestamp", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime ts { get; set; }
    }
    public class EnquiryListView
    {
        public int EnquiryId { get; set; }
        public ApplicationUser user { get; set; }
        [Display(Name = "QueryType", ResourceType = typeof(ViewRes.SharedStrings))]
        public QueryType queryType { get; set; }
        public string message { get; set; }
        [Display(Name = "Timestamp", ResourceType = typeof(ViewRes.SharedStrings))]
        public DateTime ts { get; set; }
    }
    public class ContactListView
    {
        [Display(Name = "QueryType", ResourceType = typeof(ViewRes.SharedStrings))]
        public QueryType queryType { get; set; }
        public List<Contact> messages { get; set; }
    }
}