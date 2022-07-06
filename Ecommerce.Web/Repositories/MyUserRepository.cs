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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Web.Repositories
{
    public class MyUserRepository : IMyUserRepository
    {
        private readonly IGetRequestManager _iGetRequestManager;
        private readonly IGetApiUrls _iGetApiUrls;
        private readonly IMapper _mapper;
        private readonly IPostRequestManager _iPostRequestManager;
        private readonly IPutRequestManager _iPutRequestManager;
        private readonly IDeleteRequestManager _iIDeleteRequestManager;

        public MyUserRepository(IGetRequestManager iGetRequestManager, IPostRequestManager iPostRequestManager, IPutRequestManager iPutRequestManager, IDeleteRequestManager iIDeleteRequestManager, IGetApiUrls iGetApiUrls, IMapper mapper)
        {
            _iGetRequestManager = iGetRequestManager;
            _iPostRequestManager = iPostRequestManager;
            _iPutRequestManager = iPutRequestManager;
            _iIDeleteRequestManager = iIDeleteRequestManager;
            _iGetApiUrls = iGetApiUrls;
            _mapper = mapper;

        }


        public MyUserViewModel GetMyUserById(string userId)
        {
            string apiResponse = _iGetRequestManager.SendRequest(_iGetApiUrls.GetMyUserByIdApiUrl(userId), "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<MyUser> response = new GenericResponseRepository<MyUser>();
            MyUser myUser = response.Convert(apiResponse);
            return _mapper.Map<MyUser, MyUserViewModel>(myUser);
        }

        MyUserViewModel IMyUserRepository.AddItemintoMyUser(string userId, string email, string pwd)
        {
            MyUser user = new MyUser();
            user.Id = userId;
            user.Email = email;
            user.Pwd = pwd;
            string jsonsContent = JsonConvert.SerializeObject(user);
            string apiResponse = _iPostRequestManager.SendRequest(_iGetApiUrls.PostMyUser, jsonsContent, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<MyUser> response = new GenericResponseRepository<MyUser>();
            user = response.Convert(apiResponse);
            return _mapper.Map<MyUserViewModel>(user);

        }

        MyUserViewModel IMyUserRepository.GetMyUserById(string userId)
        {
            string apiResponse = _iGetRequestManager.SendRequest(_iGetApiUrls.GetMyUserByIdApiUrl(userId), "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<MyUser> response = new GenericResponseRepository<MyUser>();
            MyUser myUser = response.Convert(apiResponse);
            return _mapper.Map<MyUser, MyUserViewModel>(myUser);
        }
    }
}
