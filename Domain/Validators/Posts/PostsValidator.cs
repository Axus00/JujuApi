using FluentValidation;
using ProjectApi.Application.DTOs;

namespace ProjectApi.Domain.Validators.Posts
{
    public class PostsValidator : AbstractValidator<PostsDto>
    {
        public PostsValidator()
        {
            RuleForEach(p => p.Posts).SetValidator(new PostValidator());
        }
    }
}