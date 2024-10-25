using Microsoft.AspNetCore.Mvc;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Posts
{
    public class PostsController : Controller
    {
        private readonly IPost _service;
        public PostsController(IPost service)
        {
            _service = service;
        }

        //endpoint
        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var post = await _service.GetPosts();
            return Ok(post);
        }

        [HttpGet("posts/{Id}")]
        public async Task<IActionResult> GetCustomer(string Id)
        {
            try
            {
                var post = await _service.GetPostById(Id);
                return Ok(post);
            }
            catch (Exceptions.IdNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}