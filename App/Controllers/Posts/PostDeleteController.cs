using Microsoft.AspNetCore.Mvc;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Shared;

namespace ProjectApi.App.Controllers.Posts
{
    public class PostDeleteController : Controller
    {
        private readonly IPost _service;
        public PostDeleteController(IPost service)
        {
            _service = service;
        }

        //endpoint
        [HttpDelete("posts/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {

            #pragma warning disable
            try
            {
                await _service.DeletePost(Id);
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