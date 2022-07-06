using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Model.Dto;

namespace Ecommerce.Service.Interface
{

    public interface IMyUserService
    {
        //Task<BasketItem> AddItemintoBasketAsync(BasketItem basketItem);
        //Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity);
        //Task<IList<BasketItem>> ClearBasketAsync(int userId);
        //Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id);
        Task<Model.Dto.MyUser> GetMyUserAsync(string userId);
        Task<MyUser> AddUserAsync(MyUser myUser);
    }
}