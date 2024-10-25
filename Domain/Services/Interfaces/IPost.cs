using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Domain.Services.Interfaces
{
    public interface IPost
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPostById(string Id);
        Task<Post> CreatePost(PostDto postDto);
        Task<List<Post>> CreatePosts(PostsDto postsDto);
        Task UpdatePosts(int Id, PostDto postDto);
        Task DeletePost(string Id);
    }
}