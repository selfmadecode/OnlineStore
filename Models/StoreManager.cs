using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class StoreManager
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}