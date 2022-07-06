using System.Collections.Generic;
using Ecommerce.Web.Model;

namespace Ecommerce.Web.Repositories
{
    public interface IProductRepository
    {
        IList<ProductViewModel> GetProducts();
        ProductViewModel GetProductById(int productId);
        ProductViewModel AddProduct(int id, string name, string desciption, decimal price, string photo);
    }
}