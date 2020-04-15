using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models
{
    public class Product
    {
        //PROPS
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Value { get; set; }

        //NAVIGATION LINKS

        public virtual List<Order> Orders { get; set; }

    }
}
