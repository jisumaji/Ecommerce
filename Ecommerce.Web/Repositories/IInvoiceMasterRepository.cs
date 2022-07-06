using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Web.Model;

namespace Ecommerce.Web.Repositories
{
    public interface IInvoiceMasterRepository
    {
        IList<InvoiceMasterViewModel> GetInvoiceMasters();
    }
}
