using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using static System.Web.HttpContext;
using SmartStore.Services;
using SmartStore.Services.Interfaces;

namespace SmartStore.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RemoveFromCart(int id)
        {
            List<UserCart> cart = (List<UserCart>)Session["Cart"];

            cart = _cartService.RemoveFromCart(id, cart);

            if (cart.Count == 0) // if item dosent exist in cart
                return View("Index");

            return View("Index");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            List<UserCart> cartItem = (List<UserCart>)Session["Cart"];

            var result = _cartService.CheckOut(cartItem);

            if (!result)
                return HttpNotFound("Something went wrong!");

            return Content("Done!");
        }

        public ActionResult AddToCart(int id)
        {
            var item = _cartService.GetOne(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            List<UserCart> cart = (List<UserCart>)Session["Cart"];

            if (Session["Cart"] == null) //destroyed this session when the user logs off
            {
                Session["Cart"] = _cartService.AddToCart(item);

                return RedirectToAction("Products", "Shop");

            }
            else
            {
                int index = _cartService.IsExist(id, cart);

                if (index != -1)
                {
                    cart[index].Quantity++;  // if an item already in the cart is added, the quantity is increased
                }
                else
                {
                    var newCartItem = _cartService.AddToCart(item);
                    cart.AddRange(newCartItem);
                }
                Session["Cart"] = cart;
                return RedirectToAction("Products", "Shop");
            }
            // */

            /*
            var allItem = context._dbContext.Items.SingleOrDefault(i => i.Id == id);

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
                    Amount = allItem.Amount,
                    UserEmail = User.Identity.GetUserName(),
                    UserId = User.Identity.GetUserId()
                });

                Session["Cart"] = cart;
                return RedirectToAction("Products", "Shop");

            }
            else
            {
                List<UserCart> cart = (List<UserCart>)Session["Cart"];

                int index = _cartService.IsExist(id, cart);

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
                return RedirectToAction("Products", "Shop");
            }
            */
        }
    }
}