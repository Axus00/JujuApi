using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectApi.Domain.Entities.Enums;

namespace ProjectApi.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Type { get; set; }
        public string? Category { get; set; }
        public int CustomerId { get; set; } //FK
        public Customer? Customer { get; set; }
    }
}