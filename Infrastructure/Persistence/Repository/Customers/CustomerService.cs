using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Entities.Enums;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Infrastructure.ExternalServices;
using ProjectApi.Shared;

namespace ProjectApi.Infrastructure.Persistence.Repository.Customers
{
    public class CustomerService : ICustomer
    {
        private readonly SqlServerContext _context; 
        private readonly IMapper _mapper;
        public CustomerService(SqlServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var dataCustomer =  await _context.Customer.AsNoTracking().ToListAsync();
            return dataCustomer;
        }

        public async Task<Customer> GetCustomerById(string Id)
        {
            var existsCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.CustomerId.ToString() == Id);

            if(existsCustomer == null)
                throw new Exceptions.IdNotFound("The customer Id not found");

            return existsCustomer;
        }

        public async Task<Customer> Create(CustomerDto customerDto)
        {
            #pragma warning disable
            var existCustomer = await _context.Customer.AsNoTracking().FirstOrDefaultAsync(c => c.Name.ToLower() == customerDto.Name.ToLower());

            if(existCustomer != null)
            {
                throw new Exceptions.CustomerIsAlready("The customer is already exists");
            }

            var mapper = _mapper.Map<Customer>(customerDto);
            _context.Customer.Add(mapper);
            await _context.SaveChangesAsync();
            return mapper;
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var existCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            if(existCustomer == null)
            {
                throw new Exceptions.IdNotFound("The customer not found or not exists");
            }
            if(!string.IsNullOrEmpty(customer.Name))
            {
                existCustomer.Name = customer.Name;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(string Id)
        {
            var existsCustomer = await _context.Customer.FirstOrDefaultAsync(p => p.CustomerId.ToString() == Id);
            if(existsCustomer == null)
            {
                throw new Exceptions.IdNotFound("The customer Id not found");
            }

            //verificamos los posts creados asociados al customer
            var associatePosts = await _context.Post.Where(p => p.CustomerId == existsCustomer.CustomerId).ToListAsync();
            if(associatePosts.Any())
            {
                _context.Post.RemoveRange(associatePosts);
            }

            existsCustomer.IsActive = false;
            existsCustomer.Status = Status.Inactivo.ToString();

            await _context.SaveChangesAsync();
        }

        
    }
}