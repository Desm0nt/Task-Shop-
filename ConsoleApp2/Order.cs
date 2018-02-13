using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Order
    {
        public string Orderid { get;}
        public string Customerid { get; }
        public string CustomerName { get; }
        public string ProductName { get;}
        public int ProductCount { get; }
        public decimal OrderCost { get; }

        public Order(string customerId,string customerName, string productName, int productCount, decimal orderCost)
        {
            Orderid = Guid.NewGuid().ToString();
            Customerid = customerId;
            CustomerName = customerName;
            ProductName = productName;
            ProductCount = productCount;
            OrderCost = orderCost;
        }
    }
}
