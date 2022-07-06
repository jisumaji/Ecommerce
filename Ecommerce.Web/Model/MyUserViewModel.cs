using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Model
{
    public class MyUserViewModel
    {

        public String Id { get; set; }
        public String Email { get; set; }
        public String Pwd { get; set; }

    }
}
