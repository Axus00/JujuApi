using Microsoft.AspNetCore.Mvc;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Customers
{
    public class CustomersController : Controller
    {
        private readonly ICustomer _service;

        public CustomersController(ICustomer service)
        {
            _service = service;
        }

        //endpoint
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _service.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("customers/{Id}")]
        public async Task<IActionResult> GetCustomer(string Id)
        {
            try
            {
                var customer = await _service.GetCustomerById(Id);
                return Ok(customer);
            }
            catch (Exceptions.IdNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}