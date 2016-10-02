using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MichellesWebsite.Models
{
    [NotMapped]
    public class ApplicationCart
    {
        public Guid Id { get; set; }
        public string Currency { get; set; }
        public string PurchaseDescription { get; set; }
        public List<ApplicationCartItem> Items { get; set; }
        public int Address { get; set; }
        private decimal totalPrice { get; set; }
        public decimal deliveryPrice { get; set; }
        public string SaleId { get; set; }
        private int weight { get; set; }

        public ApplicationCart()
        {
            Items = new List<ApplicationCartItem>();
        }

        public int Weight
        {
            get
            {
                if (Items == null)
                    return weight;
                else
                    return this.Items.Sum(x => x.Quantity);
            }
            set
            {
                // We are enabling the setting of the TotalPrice to allow for single object purchases (no cart items required)
                this.weight = value;
            }
        }

        public decimal TotalPrice 
        {
            get
            {
                if (totalPrice != 0 || this.Items.Count() == 0)
                    return totalPrice;
                else
                    return this.Items.Sum(x => x.Quantity * x.Price);
            }
            set
            {
                // We are enabling the setting of the TotalPrice to allow for single object purchases (no cart items required)
                this.totalPrice = value;
            }
        }

        public decimal SumCart
        {
            get
            {
                return this.Items.Sum(x => x.Quantity * x.Price);
            }
        }
    }
}