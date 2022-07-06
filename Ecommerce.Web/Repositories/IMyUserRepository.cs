using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Repositories
{
    public interface IMyUserRepository 
    {
        MyUserViewModel GetMyUserById(string userId);
        MyUserViewModel AddItemintoMyUser(string userId, string email, string pwd);
    }
}
