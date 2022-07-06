using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private IHostingEnvironment Environment;
        private readonly IProductRepository _iProductRepository;


        public ProductController(IProductRepository iProductRepository, IHostingEnvironment _environment)
        {
            _iProductRepository = iProductRepository;
            Environment = _environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<ProductViewModel> productViewModel = _iProductRepository.GetProducts();
            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult ProductDetail(int productId)
        {
            ProductViewModel productViewModel = _iProductRepository.GetProductById(productId);

            return View(productViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model, IFormFile Photo)
        {
            string fileName = model.Id + Path.GetExtension(Photo.FileName);// Path.GetFileName(Photo.FileName);

            ProductViewModel productViewModel = _iProductRepository.AddProduct(
                model.Id, model.Name, model.Desciption, model.Price, fileName);

            // save image to ProductImages
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "ProductImages");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            {
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    Photo.CopyTo(stream);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            return RedirectToAction("Index");
            //return View();
        }

        /*
         [HttpPost]  
public ActionResult Upload(HttpPostedFileBase file)  
{  
    if (file != null && file.ContentLength > 0)  
        try 
        {  //Server.MapPath takes the absolte path of folder 'Uploads'
            string path = Path.Combine(Server.MapPath("~/Uploads"),  
                                       Path.GetFileName(file.FileName));  
           //Save file using Path+fileName take from above string
            file.SaveAs(path);  
            ViewBag.Message = "File uploaded successfully";  
        }  
        catch (Exception ex)  
        {  
            ViewBag.Message = "ERROR:" + ex.Message.ToString();  
        }  
    else 
    {  
        ViewBag.Message = "You have not specified a file.";  
    }  
    return View();  
}
        */

    }
}
//changes made in product controller by Biyyala Satya