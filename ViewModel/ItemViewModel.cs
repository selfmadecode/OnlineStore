using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.ViewModel
{
    public class ItemViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<Category> Categories{ get; set; }
        public IEnumerable<Supplier> Supplier { get; set; }
    }
}