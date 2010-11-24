using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects.Entities;

namespace SessionPerPresenter
{
    public class CustomerListView
    {
        private readonly string _description;
        private readonly IEnumerable<Customer> _customers;
        
        public CustomerListView(string desciption, IEnumerable<Customer> customers)
        {
            _description = desciption;
            _customers = customers;
        }

        public void Show()
        {
            Console.WriteLine(_description);
            foreach (var customer in _customers)
            {
                Console.WriteLine(" * {0}", customer.LastName + ", " + customer.FirstName);
            }
        }
    }
}
