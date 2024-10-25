using FluentValidation;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Domain.Validators.Customers
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            Include(new CustomerNameRule());
        }

        //validaciones
        public class CustomerNameRule : AbstractValidator<CustomerDto>
        {
            public CustomerNameRule()
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage("The field Name is required");
            }
        }
    }
}