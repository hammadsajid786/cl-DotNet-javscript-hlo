using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Demo.Models;
namespace MVC_Demo.Controllers
{
    public class HomeController : Controller
    {
        Repo repo = new Repo();
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Products()
        {
            ListProductsToViewBag();

            return View();
        }

        [HttpPost]
        public ActionResult Products(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.AddProduct(product);
                    ViewBag.sucMessage = "Successfully Saved";
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError("", exc.Message);
                }
            }


            ListProductsToViewBag();
            return View(product);
        }

        [NonAction]
        private void ListProductsToViewBag()
        {
            try
            {
                IList<Product> products = repo.GetAllProducts();
                ViewBag.products = products;
            }
            catch (Exception exc)
            {
                ViewBag.ErrorMessage = exc.Message;
            }
        }
    }
}