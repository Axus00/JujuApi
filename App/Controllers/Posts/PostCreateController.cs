using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Posts
{
    public class PostCreateController : Controller
    {
        private readonly IPost _service;
        private readonly IValidator<PostDto> _validator;
        private readonly IValidator<PostsDto> _postsValidator;
        public PostCreateController(IPost service, IValidator<PostDto> validator, IValidator<PostsDto> postsValidator)
        {
            _service = service;
            _validator = validator;
            _postsValidator = postsValidator;
        }

        //endpoint
        [HttpPost("posts")]
        public async Task<IActionResult> CreateNewPost(PostDto postDto)
        {
            #pragma warning disable
            var postValidate = _validator.Validate(postDto);
            if(!postValidate.IsValid)
            {
                return BadRequest(postValidate.Errors.Select(e => new {e.PropertyName, e.ErrorMessage}));
            }

            try
            {
                var addPost = await _service.CreatePost(postDto);
                return Ok(addPost);
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


        [HttpPost("posts/multiple")]
        public async Task<IActionResult> CreateMultiplePost([FromBody] PostsDto postsDto)
        {
            #pragma warning disable
            var postValidate = _postsValidator.Validate(postsDto);
            if(!postValidate.IsValid)
            {
                return BadRequest(postValidate.Errors.Select(e => new {e.PropertyName, e.ErrorMessage}));
            }

            try
            {
                var addPost = await _service.CreatePosts(postsDto);
                return Ok(addPost);
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