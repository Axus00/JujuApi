using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Entities.Enums;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Infrastructure.ExternalServices;
using ProjectApi.Shared;

namespace ProjectApi.Infrastructure.Persistence.Repository.Posts
{
    public class PostService : IPost
    {
        private readonly SqlServerContext _context;
        private readonly IMapper _mapper;
        public PostService(SqlServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var post = await _context.Post.AsNoTracking()
                                            .Include(p => p.Customer)
                                            .ToListAsync();
            return post;
        }

        public async Task<Post> GetPostById(string Id)
        {
            var existsPost = await _context.Post.AsNoTracking()
                                                .Include(p => p.Customer)
                                                .FirstOrDefaultAsync(c => c.PostId.ToString() == Id);

            if(existsPost == null)
                throw new Exceptions.IdNotFound("The post Id not found");

            return existsPost;
        }

        public async Task<Post> CreatePost(PostDto postDto)
        {
            #pragma warning disable
            Customer customer = new Customer();
            var customerExists = await _context.Customer.FirstOrDefaultAsync(c => c.CustomerId == postDto.CustomerId);
            var mapper = _mapper.Map<Post>(postDto);

            if(customerExists != null)
            {
                mapper.CustomerId = postDto.CustomerId;
            }
            else
            {
                throw new Exceptions.IdNotFound("The customer isn't exists");
            }

            if(postDto.Body.Length > 20)
            {
                mapper.Body = postDto.Body.Substring(0, 17) + "...";
            }
            else
            {
                mapper.Body = postDto.Body;
            }

            //validamos de acuerdo al Enum el ingreso de la categoría
            if(Enum.IsDefined(typeof(Categories), postDto.Type))
            {
                mapper.Category = ((Categories)postDto.Type).ToString();
            }
            else
            {
                throw new ArgumentException($"The category is invalid: {postDto.Type}");
            }

            _context.Post.Add(mapper);
            await _context.SaveChangesAsync();
            return mapper;
        }

        public async Task<List<Post>> CreatePosts(PostsDto postsDto)
        {
            var post = new List<Post>();

            foreach (var postDto in postsDto.Posts)
            {
                var customerExists = await _context.Customer.FirstOrDefaultAsync(c => c.CustomerId == postDto.CustomerId);
                var mapper = _mapper.Map<Post>(postDto);

                if(customerExists != null)
                {
                    mapper.CustomerId = postDto.CustomerId;
                }
                else
                {
                    throw new Exceptions.IdNotFound("The customer isn't exists");
                }

                if(postDto.Body.Length > 20)
                {
                    mapper.Body = postDto.Body.Substring(0, 17) + "...";
                }
                else
                {
                    mapper.Body = postDto.Body;
                }

                //validamos de acuerdo al Enum el ingreso de la categoría
                if(Enum.IsDefined(typeof(Categories), postDto.Type))
                {
                    mapper.Category = ((Categories)postDto.Type).ToString();
                }
                else
                {
                    throw new ArgumentException($"The category is invalid: {postDto.Type}");
                }

                post.Add(mapper);
            }

            await _context.Post.AddRangeAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePost(string Id)
        {
            var post = await _context.Post.FirstOrDefaultAsync(p => p.PostId.ToString() == Id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePosts(int Id, PostDto postDto)
        {
            var existPost = await _context.Post.FirstOrDefaultAsync(c => c.PostId == Id);
            if(existPost == null)
            {
                throw new Exceptions.IdNotFound("The post not found or not exists");
            }

            if (!string.IsNullOrEmpty(postDto.Title))
            {
                existPost.Title = postDto.Title;
            }
            if (!string.IsNullOrEmpty(postDto.Body))
            {
                existPost.Body = postDto.Body;
            }
            if (postDto.Type.HasValue)
            {
                existPost.Type = (int)postDto.Type.Value;
            }
            if (postDto.CustomerId != 0)
            {
                existPost.CustomerId = postDto.CustomerId;
            }
            
            
            await _context.SaveChangesAsync();
        }
    }
}