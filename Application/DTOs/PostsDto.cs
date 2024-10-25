using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Application.DTOs
{
    public class PostsDto
    {
        public List<PostDto>? Posts { get; set; } = new List<PostDto>();
    }
}