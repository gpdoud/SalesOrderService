using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderService.Models {
    public class Item {

        public int Id { get; set; }
        [Required]
        [StringLength(12)]
        public string UPC { get; set; } // unique
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        public Item() {

        }
    }
}
