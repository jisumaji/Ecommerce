using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
using Ecommerce.Model.GenericRepository.Repository;
using Ecommerce.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Service
{
    public class MyUserService : IMyUserService
    {
        private readonly IRepository _iRepository;
        public MyUserService(IRepository iRepository)
        {
            _iRepository = iRepository;
        }

        public async Task<MyUser> GetMyUserAsync(string userId)
        {
            var myUser = await _iRepository.GetOneAsync<MyUser>(u => u.Id == userId);
            return myUser;
        }

        async Task<MyUser> IMyUserService.AddUserAsync(MyUser myUser)
        {
            _iRepository.Create<MyUser>(myUser);
            
            await _iRepository.SaveAsync();
            
            return myUser;

        }
    }
}
