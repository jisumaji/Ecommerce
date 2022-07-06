using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Model.Dto;

namespace Ecommerce.Service.Interface
{
    public interface IBasketService
    {
        Task<BasketItem> AddItemintoBasketAsync(BasketItem basketItem);
        Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity);
        Task<IList<BasketItem>> ClearBasketAsync(string userId);
        Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id);
        Task<IList<BasketItem>> GetBasketItemsAsync(string userId);
        Task<IList<InvoiceMaster>> GetInvoiceMastersAsync();
        Task<IList<BasketItem>> CheckoutBasketAsync(string userId);
    }
}