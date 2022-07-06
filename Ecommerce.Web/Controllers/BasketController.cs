using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketRepository _iBasketRepository;
        
        public BasketController(IBasketRepository iBasketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _iBasketRepository = iBasketRepository;
            _httpContextAccessor = httpContextAccessor;
        }        

        public IActionResult Index()
        {
            
            IList<BasketItemViewModel>  basketViewModel = _iBasketRepository.GetBasketItem(
                UserContext.getUserId(_httpContextAccessor));
            return View(basketViewModel);
        }

        [HttpPost]
        public IActionResult AddItemIntoBasket(AddBasketItemViewModel model)
        {
            BasketItemViewModel basketViewModel = _iBasketRepository.AddItemintoBasket(
                model.Id, model.Quantity, UserContext.getUserId(_httpContextAccessor)); // model.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeQuantity(int basketItemId, int quantity)
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.UpdateBasketItemQuantity(basketItemId, quantity);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.Checkout(UserContext.getUserId(_httpContextAccessor));
            return RedirectToAction("Index");
        }


        public IActionResult DeleteItemFromBasket(int basketItemId)
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.DeleteItemFromBasket(basketItemId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAllBasketItems(string userId)
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.DeleteAllBasketItems(
                UserContext.getUserId(_httpContextAccessor) //userId
                );
            return RedirectToAction("Index");
        }
        private IHttpContextAccessor _httpContextAccessor;
    }


    /// <summary>
    /// there is no user login and authentication implemented, once we have the proper login this methos will absolute
    /// </summary>
    public class UserContext
    {
        //public static string getUserId { get { return "1"; } } // Session["userid"]
        public static string getUserId(IHttpContextAccessor _httpContextAccessor) 
        { 
            return _httpContextAccessor.HttpContext.Session.GetString("userid"); 
        } // Session["userid"]
        //
        //
    }
}