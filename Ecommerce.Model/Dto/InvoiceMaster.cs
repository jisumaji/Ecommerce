using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model.Dto
{
    public class InvoiceMaster
    {
        public int Id { get; set; }

        public int bno { get; set; }

        public int productid { get; set; }

        public int quantity { get; set; }
        public DateTime shopdate { get; set; } = DateTime.Now;
        public string userid { get; set; }

        public int price { get; set; }

        public InvoiceMaster()
        {
            shopdate = DateTime.Now;
        }
    }
}
