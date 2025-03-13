using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Repository.Service
{
    public class CustomerRepository : ICustomerRepo
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Customer> _passwordHasher;
        public CustomerRepository(AppDbContext context) 
        { 
            _context = context;
            _passwordHasher = new PasswordHasher<Customer>();
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            var res = _context.Customers.FirstOrDefault(x => x.Email == customer.Email);
            if (res != null) return false;
            customer.Password = _passwordHasher.HashPassword(customer, customer.Password);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            var res = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (res == null) return false;
            res.Password = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            IEnumerable<Customer> res = _context.Customers;
            if (res == null) return null;
            return res;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var res = _context.Customers.FirstOrDefault(x => x.Email == email);
            if (res == null) return null;
            return res;
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            var res = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (res == null) return null;
            return res;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByRegistrationDateRange(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public async Task<IEnumerable<Customer>> GetTopCustomersByOrderCount(int count)
        {
            IEnumerable<Customer> res = await _context.Customers.OrderByDescending(x => x.Orders.Count).Take(count).ToListAsync();
            if (res == null) return null;
            return res;
        }

        public async Task<IEnumerable<Customer>> SearchCustomers(string name)
        {
            IEnumerable<Customer> res = await _context.Customers.Where(x => x.Name.ToLower() == name.ToLower()).ToListAsync();
            if (res == null) return null;
            return res;
        }

        public async Task<bool> UpdateCustomer(int customerId, Customer customer)
        {
            var res = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (res == null) return false;

            res.Name = customer.Name;
            res.Email = customer.Email;
            res.Address = customer.Address;
            res.Phone = customer.Phone;
            res.Password = customer.Password;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
