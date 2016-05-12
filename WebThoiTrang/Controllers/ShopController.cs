using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using WebThoiTrang.Models;
using PagedList;
namespace WebThoiTrang.Controllers
{
    public class ShopController : Controller
    {
        //
        // GET: /Shop/
        private shopDBModel data = new shopDBModel();
        private Common.Methods method = new Common.Methods();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult ViewProduct(string cat,int page,int order,int price)
        {
            var products = new List<product>();
            if (cat.ToLower() == "shirts")
            {
                products = data.products.Where(p => p.categoryID == 1).ToList<product>();
            }
            else if (cat.ToLower() == "pants")
            {
                products = data.products.Where(p => p.categoryID == 2).ToList<product>();
            }
            else if (cat.ToLower() == "all")
            {
                products = data.products.ToList<product>();
            }
            else products = null;
            if (products != null)
            {
                var quanity = products.Count;
                var pages = method.getPages(quanity, 9);
                ViewBag.Pages = pages;
                if (order == 1) return View(products.Where(p=>p.price<price).OrderByDescending(p => p.price).Skip(Math.Max(0,(9*page-9))).Take(9));
                else if (order == 2) return View(products.Where(p => p.price < price).OrderBy(p => p.price).Skip(Math.Max(0, (9 * page - 9))).Take(9));
                else return View(products.Where(p => p.price < price).OrderByDescending(p => p.productID).Skip(Math.Max(0, (9 * page - 9))).Take(9));
            }
            else return View("Error");
            
        }
        public ActionResult Detail(int id)
        {
            if(data.products.Any(p=>p.productID==id))
            {
                var productSelected = data.products.Find(id);
                return View(productSelected);
            }
            else
            {
                return View("Error");
            }    
        }
        [HttpPost]
        public void addOrder(product_odered productSelected)
        {
            method.addtoCart(productSelected);
        }
        public ActionResult order()
        {
            if (Session["product_ordered"] != null)
            {
                var lists = (List<Models.product_odered>)Session["product_ordered"];
                return View(lists);
            }
            else { return View(new List<product_odered>()); }
        }
        [HttpPost]
        public ActionResult deleteOrder(int productID)
        {
            string messeage="";
            var lists = (List<Models.product_odered>)Session["product_ordered"];
            try
            {
                if(lists.Any(p => p.product_Ordered.productID == productID))
                {
                    var producttoRemove = lists.Where(p => p.product_Ordered.productID == productID).FirstOrDefault();
                    method.removefromCart(producttoRemove);
                    messeage="succeed";
                }
                else
                {
                    messeage="This product is not in your cart!";
                }
                return Json(messeage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                messeage="Error occurs";
                return Json(messeage, JsonRequestBehavior.AllowGet);
            }  
        }
    }
}
