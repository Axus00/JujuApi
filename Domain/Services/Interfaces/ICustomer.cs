using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Domain.Services.Interfaces
{
    public interface ICustomer
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(string Id);
        Task<Customer> Create(CustomerDto customerDto);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(string Id);
    }
}