using DataAccess;
using DataObjects.Entities;
using SessionPerPresenter.Data;
//using Eg.Core;

namespace SessionPerPresenter
{

  public class ProductPresenter : IPresenter 
  {

    private readonly IDAO<Product> _productDao;

    public ProductPresenter(IDAO<Product> productDao)
    {
        _productDao = productDao;
    }

    public ProductListView ShowAllProducts()
    {
        return new ProductListView("All Products", _productDao.GetAll());
    }

    public void Dispose()
    {
        _productDao.Dispose();
    }

  }

}
