using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.ViewModel
{
    public class ItemAndCartViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<UserCart> UserCart { get; set; }
}
}