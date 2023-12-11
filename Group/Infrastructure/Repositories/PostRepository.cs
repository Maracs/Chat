using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PostRepository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public void Delete(int groupId, int id)
        {
            var post = _databaseContext.Posts.Where(post => post.Id == id).Single();
            var photos = _databaseContext.Photos.Where(photo => photo.PostId == id);
            _databaseContext.Remove(post);
            _databaseContext.Remove(photos);
        }

        public Task<List<Post>> GetAllAsync(int userId, int groupId, int offset, int limit)
        {
            return _databaseContext.Posts.Include(post => post.Photos)
                .Where(post => post.GroupId == groupId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public Task<Post> GetByIdAsync(int groupId, int id)
        {
            return _databaseContext.Posts.Include(post => post.Photos).Where(post => post.GroupId == groupId && post.Id == id).SingleAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SendAsync(Post post, List<string> photos)
        {
            await _databaseContext.Posts.AddAsync(post);
            await _databaseContext.SaveChangesAsync();

            foreach(var photoSrc in photos)
            {
                var photo = new Photo() { PostId = post.Id,Src=photoSrc}; 
                await _databaseContext.Photos.AddAsync(photo);
            }

            await _databaseContext.SaveChangesAsync();
           
        }

        public void Update(Post post)
        {
            _databaseContext.Update(post);
        }
    }
}
