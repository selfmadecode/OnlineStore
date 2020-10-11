using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Services.Interfaces
{
    public interface ICartService
    {
        List<UserCart> AddToCart(Item item);
        List<UserCart> RemoveFromCart(int id, List<UserCart> cart);
        int IsExist(int id, List<UserCart> cart);
        bool CheckOut(List<UserCart> cart);
        Item GetOne(int id);
    }
}