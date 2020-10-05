using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class UserCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        public string UserEmail { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public bool Processing { get; set; } = true;

        public Item Item { get; set; }
        public byte ItemId { get; set; }

        public ApplicationUser User { get; set; } //added
        public string UserId { get; set; }
    }
}