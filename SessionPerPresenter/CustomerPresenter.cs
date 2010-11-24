using DataAccess;
using DataObjects.Entities;
using SessionPerPresenter.Data;
//using Eg.Core;

namespace SessionPerPresenter
{

  public class CustomerPresenter : IPresenter 
  {
    private readonly IDAO<Customer> _customerDao;

    public CustomerPresenter(IDAO<Customer> customerDAO)
    {
        _customerDao = customerDAO;
    }

    public CustomerListView ShowAllCustomers()
    {
        return new CustomerListView("All Products", _customerDao.GetAll());
    }

    public void Dispose()
    {
        _customerDao.Dispose();
    }

  }

}
