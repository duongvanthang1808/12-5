using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebThoiTrang.Models;
using System.Web.Mvc;
namespace WebThoiTrang.Models
{
    public class product_odered
    {
        private product product_ordered;
        public product product_Ordered
        {
            get { return this.product_ordered; }
            set { product_ordered = value; }
        }
        private string color;
        public string Color 
        {
            get { return this.color; }
            set { color = value; }
        }
        private string size;
        public string Size
        {
            get { return this.size; }
            set { size = value; }
        }
        private int quanity;
        public int Quanity
        {
            get { return this.quanity; }
            set { quanity = value; }
        }
        public SelectList Sizes 
        {
            get
            {
                return new SelectList(this.size.Split(',').ToList<string>(), 1); ;
            }
        }
        public SelectList Colours
        {
            get
            {
                return new SelectList(this.color.Split(',').ToList<string>(),1);
            }
        }
    }
}