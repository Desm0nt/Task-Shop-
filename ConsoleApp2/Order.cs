using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Order
    {
        public struct OrderCart
        {
            public string ProductName { get; set; }
            public int ProductCount { get; set; }
            public decimal SumCost { get; set; }
        }

        public string Orderid { get;}
        public string Customerid { get; }
        public List<OrderCart> Cart { get; }
        public decimal OrderCost { get; }

        public Order(string customerId, List<OrderCart> cart)
        {
            Orderid = Guid.NewGuid().ToString();
            Customerid = customerId;
            Cart = cart;
            OrderCost = cart.Sum(x => x.SumCost);
        }
    }
}
