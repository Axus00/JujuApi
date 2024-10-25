using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Customers
{
    public class CustomerCreateController : Controller
    {
        private readonly ICustomer _service;
        private readonly IValidator<CustomerDto> _validator;
        public CustomerCreateController(ICustomer service, IValidator<CustomerDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        //endpoint
        [HttpPost("customers")]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            #pragma warning disable
            var validateCustomer = _validator.Validate(customerDto);
            if(!validateCustomer.IsValid)
            {
                return BadRequest(validateCustomer.Errors.Select(e => new {e.PropertyName, e.ErrorMessage}));
            }

            try
            {
                await _service.Create(customerDto);
                return Ok("The customer has been created successfully");
            }
            catch(Exceptions.CustomerIsAlready ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                var ProblemDetails = StatusError.CreateInternalServerError(ex);
                return StatusCode(ProblemDetails.Status.Value, ProblemDetails);
            }



        }
    }
}