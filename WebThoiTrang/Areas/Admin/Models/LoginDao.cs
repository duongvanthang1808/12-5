
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebThoiTrang.Models;

namespace ProjectClothers.Areas.Admin.Models
{
    public class LoginDao
    {
        shopDBModel context = null;

        public LoginDao()
        {
            context = new shopDBModel();
        }

        public bool CheckLogin(string username, string password)
        {
            var user = context.users.Where(m => (m.username == username) && (m.password == password)).FirstOrDefault();
            if (user != null)
                return true;
            else return false;
        }
        public List<user> getUsers()
        {
            return context.users.ToList();
        }
    }
}