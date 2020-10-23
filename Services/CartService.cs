
using Microsoft.AspNet.Identity;
using SmartStore.Models;
using SmartStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static System.Web.HttpContext;

namespace SmartStore.Services
{
    public class CartService : Controller, ICartService
    {
        readonly Context Dbcontext;
        public CartService(Context _context)
        {
            Dbcontext = _context;
        }

        public List<UserCart> AddToCart(Item item)
        {
            List<UserCart> cart = new List<UserCart>();

            cart.Add(new UserCart
            {
                ItemName = item.Name,
                Quantity = 1,
                ItemId = (byte)item.Id,
                Amount = item.Amount
            });

            return cart;
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
            if (cartItem.Count == 0)
            {
                return false;
            }

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
                    Dbcontext._dbContext.Carts.AddRange(cartItem);
                }
            }
            //Uncomment the lines below to save cart items to DB
            //context._dbContext.Carts.AddRange(cartItem);
            //context._dbContext.SaveChanges();
            return true;
        }

        public Item GetOne(int id) => Dbcontext._dbContext.Items.SingleOrDefault(i => i.Id == id);
    }
}