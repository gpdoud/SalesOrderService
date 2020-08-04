using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SalesOrderService.Models {
    public class Orderitem {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public virtual Item Item { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        public Orderitem() {

        }
    }
}
