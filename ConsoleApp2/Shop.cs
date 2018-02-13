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
            Customers =  new List<Customer>();
            Orders = new List<Order>();
        }
        
        /// <summary>
        /// Добавление продукта в базу
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="cost">Стоимость</param>
        public static void AddProduct(string name, decimal cost)
        {
            Products.Add(new Product {Name = name, Cost = cost});
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
        /// <param name="customername">Имя покупателя</param>
        /// <param name="productname">Имя продукта</param>
        /// <param name="count">Количество</param>
        private static void AddOrder(string customerid, string customername, string productname, int count, decimal cost)
        {
            Orders.Add(new Order(customerid, customername, productname, count, cost));
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
            int i = 0;
            foreach (var customer in Customers)
            {
                Console.WriteLine(i +") Name: " + customer.Name + ", Bank: " + customer.Bank + " $" );
                i++;
            }
        }

        /// <summary>
        /// Вывод товаров
        /// </summary>
        public static void ShowAllProducts()
        {
            Console.WriteLine("\n LIST OF ALL PRODUCTS");
            int i = 0;
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
        /// <param name="num">Продукт</param>
        /// <param name="count">Количество</param>
        public static void OrderProduct(string customerid, int num, int count)
        {
            string customername = Customers.Find(x => x.Customerid.Contains(customerid)).Name;
            AddOrder(customerid, customername, Products[num].Name, count, Products[num].Cost * count);
        }

        /// <summary>
        /// Вывод заказов покупателя
        /// </summary>
        public static void ShowCustomersOrders(string customerid)
        {
            Console.WriteLine("\n LIST OF ALL ORDERS");
            int i = 0;
            decimal sum = 0;
            foreach (var order in Orders)
            {
                if (order.Customerid == customerid)
                {
                    Console.WriteLine(i + ") Product Name: " + order.ProductName + ", count: " + order.ProductCount + ", cost: " + order.OrderCost + " $");
                    sum += order.OrderCost;
                    i++;
                }
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
