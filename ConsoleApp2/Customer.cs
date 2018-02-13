using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Customer
    {
        public string Name { get; }
        public decimal Bank { get; set; }
        public string Customerid { get; }

        public Customer(string name, decimal bank)
        {
            Name = name;
            Bank = bank;
            Customerid = Guid.NewGuid().ToString();
        }
    }
}
