using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Posts
{
    public class PostUpdateController : Controller
    {
        private readonly IPost _service;
        public PostUpdateController(IPost service)
        {
            _service = service;
        }

        //endpoint
        [HttpPatch("posts/{Id}")]
        public async Task<IActionResult> Update(int Id, PostDto postDto)
        {
            #pragma warning disable
            try
            {
                await _service.UpdatePosts(Id, postDto);
                return Ok("The post has been updated successfully");
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