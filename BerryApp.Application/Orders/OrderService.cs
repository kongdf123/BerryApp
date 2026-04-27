using BerryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Orders
{
    public class OrderService
    {
        private List<Order> _orders = new();

        public List<Order> GetAll() => _orders;

        public void Add(Order order) => _orders.Add(order);

        public void Delete(Order order) => _orders.Remove(order);
    }
}
