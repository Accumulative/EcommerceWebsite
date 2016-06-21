using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MichellesWebsite.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MichellesWebsite.Models
{
    [NotMapped]
    public class ProductStore
    {
        public List<ProductModel> products;
        public List<string> prices;
    }
}