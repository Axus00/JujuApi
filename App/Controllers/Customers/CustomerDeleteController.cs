using Microsoft.AspNetCore.Mvc;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Customers
{
    public class CustomerDeleteController : Controller
    {
        private readonly ICustomer _service;
        public CustomerDeleteController(ICustomer service)
        {
            _service = service;
        }

        //endpoint
        [HttpDelete("customers/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            #pragma warning disable
            try
            {
                await _service.DeleteCustomer(Id);
                return NoContent();
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