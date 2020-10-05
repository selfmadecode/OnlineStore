
using Microsoft.AspNet.Identity;
using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Web.HttpContext;

namespace SmartStore.Services
{
    public class CartService : Controller
    {
        public static Context context = new Context();



        //User clicks CheckOut
        public static bool CheckOut(List<UserCart> cartItem)
        {
            //List<UserCart> cartItem = (List<UserCart>)Session["Cart"];
            for (int i = 0; i < cartItem.Count; i++)
            {
                //if the user shops withour loggin in
                if (cartItem[i].UserEmail == "" || cartItem[i].UserId == null)
                {
                    //search the user in db 
                    cartItem[i].UserEmail = Current.User.Identity.GetUserName();
                    cartItem[i].UserId = Current.User.Identity.GetUserId();
                }
                else
                {
                    context._dbContext.Carts.AddRange(cartItem);
                }
            }

            context._dbContext.Carts.AddRange(cartItem);
            context._dbContext.SaveChanges();
            return true;
        }
    }
}