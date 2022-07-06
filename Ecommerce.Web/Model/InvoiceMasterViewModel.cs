using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Model
{
    public class InvoiceMasterViewModel
    {
        public int Id { get; set; }

        public int bno { get; set; }

        public int productid { get; set; }
        public int price { get; set; }

        public int quantity { get; set; }
        public DateTime shopdate { get; set; }
        public string userid { get; set; }
    }
}

//Gaurav Kothari
