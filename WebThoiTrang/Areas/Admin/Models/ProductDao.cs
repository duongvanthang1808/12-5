using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebThoiTrang.Models;
using PagedList;

namespace WebThoiTrang.Areas.Admin.Models
{
    public class ProductDao
    {
        shopDBModel context = null;

        public ProductDao()
        {
            context = new shopDBModel();
        }
        public IEnumerable<product> getAllProduct(int page, int pagesize)
        {
            return context.products.OrderByDescending(x => x.productID).ToPagedList(page, pagesize);

        }
        public product getDeital(int id)
        {
            return context.products.Where(m => m.productID == id).FirstOrDefault();
        }
        public int Create(product product)
        {
            var temp = context.products.Find(product.productID);

            if (temp == null)
            {
                context.products.Add(product);
                context.SaveChanges();
                return product.productID;
            }
            return 0;
        }
        public int Edit(product product, HttpPostedFileBase Image)
        {
            var temp = context.products.Find(product.productID);
            if (temp != null)
            {
                temp.productID = product.productID;
                temp.productName = product.productName;
                temp.price = product.price;
                temp.colors = product.colors;
                temp.sizes = product.sizes;
                temp.categoryID = product.categoryID;
                temp.dateCreated = product.dateCreated;
                temp.status = product.status;
                temp.quanity = product.quanity;

                if (Image != null)
                {
                    temp.productImage = product.productImage;
                }

                context.SaveChanges();
            }
            return temp.productID;
        }
    }
}