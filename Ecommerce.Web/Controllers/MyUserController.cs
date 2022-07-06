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
    public class MyUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IMyUserRepository _iMyUserRepository;
        private readonly IInvoiceMasterRepository _iInvoiceMasterRepository;

        private IHttpContextAccessor _httpContextAccessor;
        public MyUserController(
            IMyUserRepository _iMyUserRepository,
            IInvoiceMasterRepository _iInvoiceMasterRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this._iInvoiceMasterRepository = _iInvoiceMasterRepository;
            this._iMyUserRepository = _iMyUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult MyUserDetail(string userId)
        {
            MyUserViewModel myuserViewModel = _iMyUserRepository.GetMyUserById(userId);
            return View(myuserViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MyUserViewModel model)
        {
            MyUserViewModel myUserViewModel = _iMyUserRepository.AddItemintoMyUser(
                model.Id, model.Email, model.Pwd);
            return RedirectToAction("Login");
            //return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
           // return View();
        }

        [HttpPost]
        public IActionResult Login(MyUserViewModel model)
        {
            MyUserViewModel myUserViewModel = _iMyUserRepository.GetMyUserById(model.Email);

            if (model.Pwd == myUserViewModel.Pwd)
            {
                _httpContextAccessor.HttpContext.Session.SetString("userid", model.Email);
                return RedirectToAction("Index", "Product");
            }
            else 
                return RedirectToAction("Login");
            //return View();
        }


        public ViewResult AdminHome()
        {
            IList<InvoiceMasterViewModel> invoiceMasterViewModel = 
                _iInvoiceMasterRepository.GetInvoiceMasters();
            return View(invoiceMasterViewModel);
            //return View();
        }
        public ViewResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public RedirectToActionResult AdminLogin(string uid, string pwd)
        {
            if(uid=="admin" && pwd=="Secret_007")
            {
                _httpContextAccessor.HttpContext.Session.SetString("userid", "admin");
                return RedirectToAction("AdminHome");
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
    }
}
