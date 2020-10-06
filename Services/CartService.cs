
using Microsoft.AspNet.Identity;
using SmartStore.Models;
using SmartStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Web.HttpContext;

namespace SmartStore.Services
{
    public class CartService : Controller, ICartService
    {
        public static Context context = new Context();

        public bool AddToCart(int id)
        {
            var allItem = context._dbContext.Items.SingleOrDefault(i => i.Id == id);

            if (allItem == null)
            {
                return false;
            }

            if (Session["Cart"] == null) //destroyed this session when the user logs off
            {
                List<UserCart> cart = new List<UserCart>();
                cart.Add(new UserCart
                {
                    ItemName = allItem.Name,
                    Quantity = 1,
                    ItemId = (byte)allItem.Id,
                    //Id = allItem.Id,
                    Amount = allItem.Amount,
                    UserEmail = User.Identity.GetUserName(),
                    UserId = User.Identity.GetUserId()
                });

                Session["Cart"] = cart;
                return true;

            }
            else
            {
                List<UserCart> cart = (List<UserCart>)Session["Cart"];

                int index = IsExist(id, cart);

                if (index != -1)
                {
                    cart[index].Quantity++;  // if an item already in the cart is added, the quantity is increased
                }
                else
                {
                    cart.Add(new UserCart
                    {
                        ItemName = allItem.Name,
                        Quantity = 1,
                        ItemId = (byte)allItem.Id,
                        //Id = allItem.Id,
                        Amount = allItem.Amount,
                        UserEmail = User.Identity.GetUserName(),
                        UserId = User.Identity.GetUserId()
                    }); ;
                }
                Session["Cart"] = cart;
                return true;
            }
        }
        public List<UserCart> RemoveFromCart(int id, List<UserCart> cart)
        {
            int index = IsExist(id, cart);
            if (index != -1)
                cart.RemoveAt(index);
            return cart;
        }


        // checks if the cart contains an item with the same Id
        public int IsExist(int id, List<UserCart> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId == id)
                    return i;
            }
            return -1;
        }

        //User clicks CheckOut
        public bool CheckOut(List<UserCart> cartItem)
        {
            if(cartItem.Count == 0)
            {
                return false;
            }
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

            //context._dbContext.Carts.AddRange(cartItem);
            //context._dbContext.SaveChanges();
            return true;
        }
    }
}