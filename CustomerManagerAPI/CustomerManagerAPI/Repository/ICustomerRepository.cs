using System.Collections.Generic;
using System.Linq;
using CustomerManagerAPI.Model;

namespace CustomerManagerAPI.Repository
{
    public interface ICustomerRepository
    {
        OperationStatus CheckUnique(int id, string property, string value);
        OperationStatus DeleteCustomer(int id);
        Customer GetCustomerById(int id);
        IQueryable<Customer> GetCustomers();
        IQueryable<CustomerSummary> GetCustomersSummary(out int totalRecords);
        List<State> GetStates();
        OperationStatus InsertCustomer(Customer customer);
        OperationStatus UpdateCustomer(Customer customer);
    }
}