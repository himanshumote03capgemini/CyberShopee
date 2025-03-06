using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;

namespace CyberShopee.Repository.Service
{
    public class CustomerRepository : ICustomerRepo
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) { _context = context; }

        public async Task<bool> AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetCustomersByRegistrationDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetTopCustomersByOrderCount(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> SearchCustomers(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCustomer(int customerId, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
