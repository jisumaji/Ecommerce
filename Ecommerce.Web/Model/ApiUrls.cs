using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Model
{

    public class ApiUrls
    {
        public string BaseUrl { get; set; }
        public string GetProducts { get; set; }
        public string GetInvoiceMasters { get; set; }
        public string GetProductbyId { get; set; }
        public string GetBasketItem { get; set; }
        public string PostBasketItem { get; set; }
        public string PostMyUser { get; set; }
        public string PostProduct { get; set; }
        public string PutChangeItemQuantity { get; set; }
        public string DeleteBasketItems { get; set; }
        public string Checkout { get; set; }
        public string DeleteItemFromBasket { get; set; }

        public string GetMyUserById { get; set; }
    }

    public class GetApiUrls : IGetApiUrls
    {
        private readonly IOptions<ApiUrls> _apiUrls;
        public GetApiUrls(IOptions<ApiUrls> apiUrls)
        {
            _apiUrls = apiUrls;
        }

        public string ProductsApiUrl => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetProducts;

        public string GetProductByIdApiUrl(int productId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetProductbyId + productId;
        public string GetMyUserByIdApiUrl(string userId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetMyUserById + userId;

        public string GetBasketItem(string userId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetBasketItem + "/"+userId;

        public string PostBasketItem => _apiUrls.Value.BaseUrl + _apiUrls.Value.PostBasketItem;
        public string PostMyUser => _apiUrls.Value.BaseUrl + _apiUrls.Value.PostMyUser;
        public string PostProduct => _apiUrls.Value.BaseUrl + _apiUrls.Value.PostProduct;

        string IGetApiUrls.InvoiceMastersApiUrl => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetInvoiceMasters;


        public string PutChangeItemQuantity(int basketItemId, int quantity) => _apiUrls.Value.BaseUrl + _apiUrls.Value.PutChangeItemQuantity + basketItemId + "/" + quantity;

        public string DeleteBasketItems(string userId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.DeleteBasketItems + "/" + userId;
        public string Checkout(string userId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.Checkout + "/" + userId;

        public string DeleteItemFromBasket(int basketItemId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.DeleteItemFromBasket + basketItemId;

    } 

}
