using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static string _currentCustomerId;

        static void Main(string[] args)
        {
            //add products
            Shop.AddProduct("Ktulhu", 200);
            Shop.AddProduct("Azathoth", 120);
            Shop.AddProduct("Daoloth", 45);
            Shop.AddProduct("Shub-Niggurath", 170);
            Shop.AddProduct("The Blackness from the Stars", 11);
            Shop.AddProduct("Nyctelios", 100);
            Shop.AddProduct("Nyarlathotep", 600);

            //add customers
            Shop.AddCustomer("Zevs", 1000);
            Shop.AddCustomer("Thor", 1700);
            Shop.AddCustomer("Hel", 5000);
            Shop.AddCustomer("Perun", 200);

            Shop.ShowAllCustomers();
            Console.WriteLine("\n\n  Please select your customer number.");
            int num = 0;
            do
            {
                var customerString = Console.ReadLine();
                if (int.TryParse(customerString, out num))
                {
                    if (num < 1 || num > Shop.Customers.Count)
                        Console.WriteLine("Customer not exist. Number must between 1 and " + Shop.Customers.Count + ".");
                }
                else
                    Console.WriteLine("Number must be int32. Please try again.");
            } while (num < 1 || num > Shop.Customers.Count);

            _currentCustomerId = Shop.SelectCustomer(num-1);           

            string key;
            do
            {
                Console.WriteLine("\nPress O to make order, L to list orders, P to checkout, D to delete order, S to show customers info or E for exit.");
                var clk = Console.ReadKey();
                key = clk.Key.ToString();
                switch (key)
                {
                    case "O":
                        Shop.ShowAllProducts();
                        MakeOrder();
                        break;
                    case "L":
                        Shop.ShowCustomersOrders(_currentCustomerId);
                        break;
                    case "D":
                        var customerorders = Shop.Orders.Where(x => x.Customerid.Contains(_currentCustomerId)).ToList();
                        DeleteOrder(customerorders);
                        break;
                    case "P":
                        Shop.Checkout(_currentCustomerId);
                        break;
                    case "S":
                        Console.WriteLine("\n");
                        Shop.ShowAllCustomers();
                        break;
                }
            } while (key != "E");
        }


        //make an order
        static void MakeOrder()
        {
            bool choosed = false;
            List<Order.OrderCart> cart = new List<Order.OrderCart>();
            string key;
            do
            {
                Console.WriteLine("\nPress A to add product to cart or C to place an order.");
                var clk = Console.ReadKey();
                key = clk.Key.ToString();
                switch (key)
                {
                    case "A":
                        Console.WriteLine("\n  Please select a product number.");
                        int num = 0;
                        do
                        {
                            var productString = Console.ReadLine();
                            if (int.TryParse(productString, out num))
                            {
                                if (num < 1 || num > Shop.Products.Count)
                                    Console.WriteLine("Product not exist. Number must between 1 and " + Shop.Products.Count + ".");
                            }
                            else
                                Console.WriteLine("Number must be int32. Please try again.");
                        } while (num < 1 || num > Shop.Products.Count);

                        Console.WriteLine("  Please select products count.");
                        int col = CheckedValue();
                        cart.Add( new Order.OrderCart {ProductName = Shop.Products[num - 1].Name, ProductCount  = col, SumCost = Shop.Products[num - 1].Cost * col});
                        Console.WriteLine("\n You add to cart " + col + " " + Shop.Products[num - 1].Name);
                        choosed = true;
                        break;
                    case "C":
                        if (choosed == true)
                        {
                            Shop.OrderProduct(_currentCustomerId, cart);
                            Console.WriteLine("\n Order successfuly placed.");
                            key = "E";
                        }
                        else
                        {
                            Console.WriteLine("\n Nothin to order.");
                            key = "E";
                        }
                        break;
                }
            } while (key != "E");
        }

        static void DeleteOrder(List<Order> orders)
        {
            Shop.ShowCustomersOrders(_currentCustomerId);
            Console.WriteLine("\n  Please select an order number to delete.");
            int num = 0;
            do
            {
                var productString = Console.ReadLine();
                if (int.TryParse(productString, out num))
                {
                    if (num < 1 || num > orders.Count)
                        Console.WriteLine("Order not exist. Number must between 1 and " + orders.Count + ".");
                }
                else
                    Console.WriteLine("Number must be int32. Please try again.");
            } while (num < 1 || num > orders.Count);

            Shop.RemoveOrder(orders[num-1].Orderid);
        }

        static int CheckedValue()
        {
            int j = 0;
            do
            {
                var colString = Console.ReadLine();
                try
                {
                    int.TryParse(colString, out j);
                    if (j <= 0)
                    {
                        Console.WriteLine("Must be more than 0.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Must be int32. Please try again.");
                }
            } while (j <= 0);
            return j;
        }
    }
}
