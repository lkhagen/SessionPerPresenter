using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects.Entities;

namespace SessionPerPresenter
{
    public class ProductListView
    {
        private readonly string _description;
        private readonly IEnumerable<Product> _products;

        public ProductListView(string desciption, IEnumerable<Product> products)
        {
            _description = desciption;
            _products = products;
        }

        public void Show()
        {
            Console.WriteLine(_description);
            foreach (var product in _products)
            {
                Console.WriteLine(" * {0}", product.Name);
            }
        }
    }
}
