using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderService.Models {
    public class Customer {

        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(2)]
        public string State { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Sales { get; set; }
        public bool Active { get; set; }

        public Customer() {

        }
    }
}
