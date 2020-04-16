using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Adress { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [Required]
        public DateTime DelivTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        //NAVIGATION LINKS

        public virtual List<Product> Products { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nAdress: {Adress}\nEmail: {Email}\nMobile: {Mobile}\nDelivery Time: {DelivTime}\nStatus: {Status}";
        }
    }
}
