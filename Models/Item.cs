using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name cannot be empty")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(int), "1", "100000", ErrorMessage = "Quantity range is between 1-100000")]
        public int Quantity { get; set; }

        [Required]
        [Range(typeof(double), "1", "1000000000000", ErrorMessage ="Amount should be greater than 0 or less than a billion" )]
        public double Amount { get; set; }

        [Display(Name = "Expiration Date")] // what is shown on the form label
        public DateTime? ExpiringDate { get; set; }

        [Required]
        public byte Image { get; set; }


        // Category Navigation prop
        public Category Category { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a Category")]
        public byte CategoryId { get; set; }


        // Supplier Navigation Prop
        public Supplier Supplier { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please select a Supplier")]
        public byte SupplierId { get; set; }
    }
}