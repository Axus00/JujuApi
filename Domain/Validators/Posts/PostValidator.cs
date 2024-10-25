using FluentValidation;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities.Enums;

namespace ProjectApi.Domain.Validators.Posts
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            Include(new PostTitleRule());
            Include(new PostBodyRule());
            Include(new PostTypeRule());
            Include(new PostCustomerIdRule());
        }

        //validaciones
        public class PostTitleRule : AbstractValidator<PostDto>
        {
            public PostTitleRule()
            {
                RuleFor(p => p.Title).NotEmpty()
                                    .WithMessage("The field Title musn't be empty");
            }
        }

        public class PostBodyRule : AbstractValidator<PostDto>
        {
            public PostBodyRule()
            {
                RuleFor(p => p.Body).NotEmpty()
                                    .WithMessage("The field is required ");
                                    
            }
        }

        public class PostTypeRule : AbstractValidator<PostDto>
        {
            public PostTypeRule()
            {
                RuleFor(p => p.Type).NotEmpty()
                                    .Must(type => type != null && Enum.IsDefined(typeof(Categories), type.Value))
                                    .WithMessage("The field is required, and must be 'Farandula', 'Futbol', or 'Politica' ");
                                    
            }
        }
    }

    internal class PostCustomerIdRule : AbstractValidator<PostDto>
    {
        public PostCustomerIdRule()
        {
            RuleFor(p => p.CustomerId).NotEmpty()
                                    .WithMessage("The field is required, and must be related with any customer");
        }
    }
}