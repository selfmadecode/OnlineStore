using SmartStore.Models;
using SmartStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Services
{
    public interface IShopService
    {
        List<Item> GetAllProducts(); //

        Item GetItemById(int id);

        List<Category> GetAllCategories();

        List<Supplier> GetAllSuppliers();

        void AddItemToDb(Item item); //

        Item DeleteItem(int id); //

        List<Item> SearchDb(string search); //

        Item GetItemDetails(int id); //

        void UpdateItemInDb(Item item); //
        ItemViewModel EditItemInDb(int id); //

        ItemViewModel ItemViewModel(Item item = null); //
    }
}