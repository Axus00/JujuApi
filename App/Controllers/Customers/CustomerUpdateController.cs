using Microsoft.AspNetCore.Mvc;
using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Customers
{
    public class CustomerUpdateController : Controller
    {
        private readonly ICustomer _service;
        public CustomerUpdateController(ICustomer service)
        {
            _service = service;
        }

        //endpoint
        [HttpPatch("customers")]
        public async Task<IActionResult> UpdateField(Customer customer)
        {
            #pragma warning disable
            try
            {
                await _service.UpdateCustomer(customer);
                return Ok("The customer has been updated successfully");
            }
            catch(Exceptions.IdNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                var ProblemDetails = StatusError.CreateInternalServerError(ex);
                return StatusCode(ProblemDetails.Status.Value, ProblemDetails);
            }
        }
    }
}