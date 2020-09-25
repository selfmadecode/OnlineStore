using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace SmartStore.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _dbContext;
        public CartController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Cart
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToCart( int id)
        {
            var allItem = _dbContext.Items.SingleOrDefault(i => i.Id == id);

            if (allItem == null)
            {
                return HttpNotFound();
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
                    UserEmail = User.Identity.Name //get user email
                });

                Session["Cart"] = cart;
                return RedirectToAction("Products", "Shop");

            }
            else
            {
                List<UserCart> cart = (List<UserCart>)Session["Cart"];

                int index = isExist(id);

                if (index != -1)
                {
                    cart[index].Quantity++;  // if an item already in the cart is added, the quantity is increased
                }
                else
                {
                    cart.Add(new UserCart { 
                        ItemName = allItem.Name,
                        Quantity = 1,
                        ItemId = (byte)allItem.Id,
                        //Id = allItem.Id,
                        Amount = allItem.Amount,
                        UserEmail = User.Identity.Name
                    });
                }
                Session["Cart"] = cart;
                return RedirectToAction("Products", "Shop");
            }
        }

        public ActionResult RemoveFromCart(int id)
        {
            // removes the item
            List<UserCart> cart = (List<UserCart>)Session["Cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["Cart"] = cart;
            return View("Index");
        }

        // checks if the cart contains an item with the same Id
        private int isExist(int id)
        {
            List<UserCart> cart = (List<UserCart>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                    return i;
            }
         return -1;
        }
        public ActionResult Checkout()
        {
            List<UserCart> cartItem = (List<UserCart>)Session["Cart"];
            UserCart cart = new UserCart();

            foreach (var item in cartItem)
            {
                cart.ItemName = item.ItemName;
                cart.Quantity = item.Quantity;
                cart.Amount = item.Amount;
                cart.UserEmail = item.UserEmail;
                cart.ItemId = item.ItemId;
                cart.User.Id = item.User.Id; //to get the logged in user
            }
            _dbContext.Carts.Add(cart);
            _dbContext.SaveChanges(); //not saving, id is 0
            return Content("Done!");
        }
    }
}