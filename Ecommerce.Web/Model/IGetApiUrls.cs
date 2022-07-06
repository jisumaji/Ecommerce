namespace Ecommerce.Web.Model
{
    public interface IGetApiUrls
    {

        string GetBasketItem(string userId);
        string PostBasketItem { get; }
        string PostMyUser { get; }
        string PostProduct { get; }
        string ProductsApiUrl { get; }
        string InvoiceMastersApiUrl { get; }

        string DeleteItemFromBasket(int basketItemId);
        string GetProductByIdApiUrl(int productId);
        string PutChangeItemQuantity(int basketItemId, int quantity);
        string DeleteBasketItems(string userId);

        string Checkout(string userId);

        string GetMyUserByIdApiUrl(string userId);
    }
}