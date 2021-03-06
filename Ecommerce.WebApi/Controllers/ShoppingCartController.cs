using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Model.Dto;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Ecommerce.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IBasketService _iBasketService;
        public ShoppingCartController(IBasketService iBasketService)
        {
            _iBasketService = iBasketService;
        }

        // GET: api/ShoppingCart/GetBasketItems
        [HttpGet("GetBasketItems/{userId}")]
        public async Task<IActionResult> GetBasketItems(string userId)
        {
            IList<BasketItem> basketItems = await _iBasketService.GetBasketItemsAsync(userId);           
            return Ok(basketItems);           
        }


        // GET: api/ShoppingCart/GetBasketItems
        [HttpGet("GetInvoiceMasters")]
        public async Task<IActionResult> GetInvoiceMasters()
        {
            IList<InvoiceMaster> invoiceMasters= 
                await _iBasketService.GetInvoiceMastersAsync();
            return Ok(invoiceMasters);
        }



        //// POST api/ShoppingCart
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody]
            BasketItem basketItem)
        {
            await _iBasketService.AddItemintoBasketAsync(basketItem);
            return Created($"ShoppingCart", basketItem);
        }

        //// POST api/ShoppingCart
        //[HttpPost]
        //public async Task<IActionResult> Post(
        //    //[FromBody]
        //    int id, int ProductId, int Quantity, string UserId)
        //{
        //    BasketItem basketItem = new BasketItem() { Id = id, ProductId = ProductId, Quantity = Quantity, UserId = UserId };

        //    await _iBasketService.AddItemintoBasketAsync(basketItem);
        //    return Created($"ShoppingCart", basketItem);
        //}

        // PUT api/ShoppingCart/ChangeItemQuantity/5/4
        [HttpPut("ChangeItemQuantity/{basketItemId}/{quantity}")]
        public async Task<IActionResult> ChangeItemQuantity(int basketItemId, int quantity)
        {
            IList<BasketItem> basketItems = await _iBasketService.ChangeBasketItemQuantityAsync(basketItemId, quantity);
            if (basketItems == null)
                return NotFound("Item not found in the basket, please check the basketItemId"); 
            return Ok(basketItems);
        }

        // DELETE api/ShoppingCart/ClearBasket/1
        [HttpDelete("ClearBasket/{userId}")]
        public async Task<IActionResult> ClearBasket(string userId)
        {
            IList<BasketItem> basketItems = await _iBasketService.ClearBasketAsync(userId);
            return Ok(basketItems);
        }

        // DELETE api/ShoppingCart/ClearBasket/1
        [HttpGet("Checkout/{userId}")]
        public async Task<IActionResult> Checkout(string userId)
        {
            Console.WriteLine(userId);
            //IList<BasketItem> basketItems = await _iBasketService.ClearBasketAsync(userId);
            IList<BasketItem> basketItems = await _iBasketService.CheckoutBasketAsync(userId);
            return Ok(basketItems);
        }

        // DELETE api/ShoppingCart/DeleteItemFromBasket/5
        [HttpDelete("DeleteItemFromBasket/{basketItemId}")]
        public async Task<IActionResult> DeleteItemFromBasket(int basketItemId)
        {
            IList<BasketItem> basketItems = await _iBasketService.DeleteBasketItemByIdAsync(basketItemId);
            if (basketItems == null)
                return NotFound("Item not found in the basket, please check the basketItemId");
            return Ok(basketItems);
        }
    }
}