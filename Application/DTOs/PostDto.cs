using ProjectApi.Domain.Entities.Enums;

namespace ProjectApi.Application.DTOs
{
    public class PostDto
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public Categories? Type { get; set; }
        public int CustomerId { get; set; }
    }
}