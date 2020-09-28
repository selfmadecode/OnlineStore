using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class StoreManager
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}