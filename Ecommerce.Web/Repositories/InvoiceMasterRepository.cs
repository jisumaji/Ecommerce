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
    public class InvoiceMasterRepository : IInvoiceMasterRepository
    {
        private readonly IGetRequestManager _iGetRequestManager;
        private readonly IGetApiUrls _iGetApiUrls;
        private readonly IMapper _mapper;
        private readonly IPostRequestManager _iPostRequestManager;
        private readonly IPutRequestManager _iPutRequestManager;
        private readonly IDeleteRequestManager _iIDeleteRequestManager;
        public InvoiceMasterRepository(
            IGetRequestManager iGetRequestManager, 
            IPostRequestManager iPostRequestManager, 
            IPutRequestManager iPutRequestManager, 
            IDeleteRequestManager iIDeleteRequestManager, IGetApiUrls iGetApiUrls, IMapper mapper)
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
        public IList<InvoiceMasterViewModel> GetInvoiceMasters()
        {

            string apiResponse = _iGetRequestManager.SendRequest(_iGetApiUrls.InvoiceMastersApiUrl, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<InvoiceMaster> response = new GenericResponseRepository<InvoiceMaster>();
            IList<InvoiceMaster> product = response.Convert_to_List(apiResponse);
            return _mapper.Map<IList<InvoiceMasterViewModel>>(product);
        }

    }
}
