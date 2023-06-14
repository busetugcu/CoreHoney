using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int HoneyId { get; set; }
        public Honey Honey { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}
