using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderService.Models {
    public class Order {

        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Order() {

        }
    }
}
