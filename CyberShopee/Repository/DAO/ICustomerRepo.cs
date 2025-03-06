using CyberShopee.Models;

namespace CyberShopee.Repository.DAO
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<Customer> GetCustomerById(int customerId);

        Task<bool> AddCustomer(Customer customer);

        Task<bool> UpdateCustomer(int customerId, Customer customer);

        Task<bool> DeleteCustomer(int customerId);

        Task<Customer> GetCustomerByEmail(string email);

        Task<IEnumerable<Customer>> SearchCustomers(string name);

        Task<IEnumerable<Customer>> GetCustomersByRegistrationDateRange(DateTime startDate, DateTime endDate);

        Task<IEnumerable<Customer>> GetTopCustomersByOrderCount(int count);
    }
}
