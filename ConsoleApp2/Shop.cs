using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    static class Shop
    {
        public static List<Product> Products;
        public static List<Customer> Customers;
        public static List<Order> Orders;

        static Shop()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Orders = new List<Order>();
        }

        /// <summary>
        /// Добавление продукта в базу
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="cost">Стоимость</param>
        public static void AddProduct(string name, decimal cost)
        {
            Products.Add(new Product { Name = name, Cost = cost });
        }

        /// <summary>
        /// Добавление покупателя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="bank">Баланс</param>
        public static void AddCustomer(string name, decimal bank)
        {
            Customers.Add(new Customer(name, bank));
        }

        /// <summary>
        /// Оформление заказа в корзину
        /// </summary>
        /// <param name="customerid">ID покупателя</param>
        /// <param name="cart">Корзина заказа</param>
        private static void AddOrder(string customerid, List<Order.OrderCart> cart)
        {
            Orders.Add(new Order(customerid, cart));
        }

        /// <summary>
        /// Выбор покупателя для работы
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string SelectCustomer(int num)
        {
            return Customers[num].Customerid;
        }

        /// <summary>
        /// Вывод всех покупателей
        /// </summary>
        public static void ShowAllCustomers()
        {
            int i = 1;
            foreach (var customer in Customers)
            {
                Console.WriteLine(i + ") Name: " + customer.Name + ", Bank: " + customer.Bank + " $");
                i++;
            }
        }

        /// <summary>
        /// Вывод товаров
        /// </summary>
        public static void ShowAllProducts()
        {
            Console.WriteLine("\n LIST OF ALL PRODUCTS");
            int i = 1;
            foreach (var product in Products)
            {
                Console.WriteLine(i + ") Name: " + product.Name + ", cost: " + product.Cost + " $");
                i++;
            }
        }

        /// <summary>
        /// Оформление заказа
        /// </summary>
        /// <param name="customerid">ID покупателя</param>
        /// <param name="cart">Корзина заказа</param>
        public static void OrderProduct(string customerid, List<Order.OrderCart> cart)
        {
            //string customername = Customers.Find(x => x.Customerid.Contains(customerid)).Name;
            AddOrder(customerid, cart);
        }

        /// <summary>
        /// Вывод заказов покупателя
        /// </summary>
        public static void ShowCustomersOrders(string customerid)
        {
            Console.WriteLine("\n LIST OF ALL ORDERS");
            int i = 1;
            decimal sum = 0;
            var customerorders = Orders.Where(x => x.Customerid.Contains(customerid)).ToList();
            foreach (var order in customerorders)
            {
                Console.WriteLine(i + " Order: ");
                foreach (var stash in order.Cart)
                {
                    Console.WriteLine("Name: " + stash.ProductName + ", Count: " + stash.ProductCount + ", Summary cost: " + stash.SumCost + " $");
                }
                sum += order.OrderCost;
                i++;
            }
            Console.WriteLine("Total cost: " + sum + " $");
        }

        /// <summary>
        /// удаление заказа
        /// </summary>
        /// <param name="orderid"></param>
        public static void RemoveOrder(string orderid)
        {
            Orders.RemoveAll(x => x.Orderid.Contains(orderid));
            Console.WriteLine("\nOrder is successfully deleted.");
        }

        /// <summary>
        /// Расчет за покупки
        /// </summary>
        /// <param name="customerid"></param>
        public static void Checkout(string customerid)
        {
            var customerorders = Orders.Where(x => x.Customerid.Contains(customerid));
            decimal sum = customerorders.Sum(x => x.OrderCost);
            var customer = Customers.Find(x => x.Customerid.Contains(customerid));
            if (sum <= customer.Bank)
            {
                customer.Bank -= sum;
                Orders.RemoveAll(x => x.Customerid.Contains(customerid));
                Console.WriteLine("\nPayment is successfully completed.");
            }
            else
                Console.WriteLine("\nNot enough money! You must pay " + sum + " $, but have only " + customer.Bank + " $");
        }
    }
}
