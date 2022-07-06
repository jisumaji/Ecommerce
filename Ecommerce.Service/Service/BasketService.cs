using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
using Ecommerce.Model.GenericRepository.Repository;
using Ecommerce.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Service
{
    public class BasketService : IBasketService
    {
        private readonly IRepository _iRepository;
        public BasketService(IRepository iRepository)
        {
            _iRepository = iRepository;
        }

        public async Task<BasketItem> AddItemintoBasketAsync(BasketItem basketItem)
        {
            IEnumerable<BasketItem> basketItems = await _iRepository.GetAsync<BasketItem>();//b => b.UserId == basketItem.UserId);

            if (basketItems.Count() > 0)
            {
                basketItem.Id = basketItems.Last().Id + 1;
            }
            else
            {
                basketItem.Id = 1;
            }

            try
            {
                //basketItem.Id = 10;
                _iRepository.Create<BasketItem>(basketItem);
                await _iRepository.SaveAsync();
            }
            catch (Exception exp) {
                Console.WriteLine("error "+exp.ToString());
            }

            return basketItem;
        }

        public async Task<IList<BasketItem>> GetBasketItemsAsync(string userId)
        {
            var basketItems = await _iRepository.GetAsync<BasketItem>(b => b.UserId == userId);
            basketItems = PopulateProductIntoBasketItem(basketItems.ToList());
            return basketItems.ToList();
        }

        public async Task<IList<BasketItem>> ClearBasketAsync(string userId)
        {
            var basketItems = await _iRepository.GetAsync<BasketItem>(b => b.UserId == userId);

            foreach (var basketItem in basketItems)
            {
                _iRepository.Delete<BasketItem>(basketItem);
            }
            await _iRepository.SaveAsync();

            return await GetBasketItemsAsync(userId);
        }
        public async Task<IList<BasketItem>> CheckoutBasketAsync(string userId)
        {
            var basketItems = await _iRepository.GetAsync<BasketItem>(b => b.UserId == userId);

            Random random = new Random();
            int sno = random.Next();
            foreach (var basketItem in basketItems)
            {
                InvoiceMaster invoiceMaster = new InvoiceMaster();
                invoiceMaster.bno = sno;
                invoiceMaster.productid = basketItem.ProductId;
                invoiceMaster.quantity = basketItem.Quantity;
                invoiceMaster.userid = basketItem.UserId;
                //invoiceMaster.price = basketItem.
                invoiceMaster.userid = basketItem.UserId;

                _iRepository.Create<InvoiceMaster>(invoiceMaster);
                _iRepository.Delete<BasketItem>(basketItem);
            }
            await _iRepository.SaveAsync();

            return await GetBasketItemsAsync(userId);

        }

        public async Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id)
        {
            var basketItem = await _iRepository.GetByIdAsync<BasketItem>(id);
            if (basketItem != null)
            {
                _iRepository.Delete<BasketItem>(basketItem);
                await _iRepository.SaveAsync();
            }

            return await GetBasketItemsAsync(basketItem.UserId);
        }

        public async Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity)
        {
            var basketItem = await _iRepository.GetByIdAsync<BasketItem>(id);

            if (basketItem == null)
                return null;

            basketItem.Quantity = quantity;

            _iRepository.Update<BasketItem>(basketItem);
            await _iRepository.SaveAsync();
            return await GetBasketItemsAsync(basketItem.UserId);
        }

        #region Private Helper
        /// <summary>
        /// this method will be absolute when we have the actuall database, in memory database does not supoort relation database
        /// </summary>
        private List<BasketItem> PopulateProductIntoBasketItem(List<BasketItem> basketItems)
        {
            foreach (var basketItem in basketItems)
            {
                basketItem.Product = _iRepository.GetById<Product>(basketItem.ProductId);
            }

            return basketItems;
        }
        #endregion

        public async Task<IList<InvoiceMaster>> GetInvoiceMastersAsync()
        {
            var invoiceMasters = await _iRepository.GetAllAsync<InvoiceMaster>();
            return invoiceMasters.ToList();
        }


    }
}
