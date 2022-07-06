using Ecommerce.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Ecommerce.WebApi.Client.Interface;
using static Ecommerce.WebApi.Client.Helper.HelperEnum;
using Ecommerce.WebApi.Client.Repository;
using Ecommerce.WebApi.Client.ExtentionMethod;
using Newtonsoft.Json;

namespace Ecommerce.Web.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGetRequestManager _iGetRequestManager;
        private readonly IGetApiUrls _iGetApiUrls;
        private readonly IMapper _mapper;
        private readonly IPostRequestManager _iPostRequestManager;
        private readonly IPutRequestManager _iPutRequestManager;
        private readonly IDeleteRequestManager _iIDeleteRequestManager;
        public ProductRepository(IGetRequestManager iGetRequestManager, IPostRequestManager iPostRequestManager, IPutRequestManager iPutRequestManager, IDeleteRequestManager iIDeleteRequestManager, IGetApiUrls iGetApiUrls, IMapper mapper)
        {
            _iGetRequestManager = iGetRequestManager;
            _iPostRequestManager = iPostRequestManager;
            _iPutRequestManager = iPutRequestManager;
            _iIDeleteRequestManager = iIDeleteRequestManager;
            _iGetApiUrls = iGetApiUrls;
            _mapper = mapper;

        }

        /// <summary>
        /// Get product catelog
        /// </summary>
        /// <returns>List of all product view model</returns>
        public IList<ProductViewModel> GetProducts()
        {

            string apiResponse = _iGetRequestManager.SendRequest(_iGetApiUrls.ProductsApiUrl, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<Product> response = new GenericResponseRepository<Product>();
            IList<Product> product = response.Convert_to_List(apiResponse);
            return _mapper.Map<IList<ProductViewModel>>(product);
        }

        /// <summary>
        /// Get single product by product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>single product view model</returns>
        public ProductViewModel GetProductById(int productId)
        {
            string apiResponse = _iGetRequestManager.SendRequest(_iGetApiUrls.GetProductByIdApiUrl(productId), "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<Product> response = new GenericResponseRepository<Product>();
            Product product = response.Convert(apiResponse);
            return _mapper.Map<Product, ProductViewModel>(product);
        }

        ProductViewModel IProductRepository.AddProduct(int id, string name, string desciption, decimal price, string photo)
        {
            Product product = new Product();
            product.Id = id;
            product.Name = name;
            product.Desciption = desciption;
            product.Price = price;
            product.Photo = photo;
            product.Price = price;
            string jsonsContent = JsonConvert.SerializeObject(product);
            string apiResponse = _iPostRequestManager.SendRequest(
                _iGetApiUrls.PostProduct, jsonsContent, "", "", false, 
                HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<Product> response = new GenericResponseRepository<Product>();
            product = response.Convert(apiResponse);
            return _mapper.Map<ProductViewModel>(product);

        }
    }
}
