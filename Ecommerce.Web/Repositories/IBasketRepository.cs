using Ecommerce.Web.Model;
using System.Collections.Generic;

namespace Ecommerce.Web.Repositories
{
    public interface IBasketRepository
    {
        IList<BasketItemViewModel> GetBasketItem(string userId);
        BasketItemViewModel AddItemintoBasket(int productId, int quantity, string userId);
        IList<BasketItemViewModel> UpdateBasketItemQuantity(int basketItemId, int quantity);
        IList<BasketItemViewModel> DeleteItemFromBasket(int basketItemId);
        IList<BasketItemViewModel> DeleteAllBasketItems(string userId);
        IList<BasketItemViewModel> Checkout(string v);
    }
}