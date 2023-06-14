using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.Entities
{
    public class OrderItem
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int HoneyId { get; set; }
        public Honey Honey { get; set; }
    }
}
