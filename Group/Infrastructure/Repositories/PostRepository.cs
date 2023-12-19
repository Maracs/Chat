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

        public Task<List<Post>> GetAllAsync(int userId, int groupId, int offset, int limit, CancellationToken token)
        {
            return _databaseContext.Posts.Include(post => post.Photos)
                .Where(post => post.GroupId == groupId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(token);
        }

        public Task<Post> GetByIdAsync(int groupId, int id)
        {
            return _databaseContext.Posts.Include(post => post.Photos).Where(post => post.GroupId == groupId && post.Id == id).SingleAsync();
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            await _databaseContext.SaveChangesAsync(token);
        }

        public async Task SendAsync(Post post, List<string> photoSrcs)
        {
            await _databaseContext.Posts.AddAsync(post);
            await _databaseContext.SaveChangesAsync();
            var photos = photoSrcs.Select(photoSrc => new Photo() { PostId = post.Id, Src = photoSrc });
            await _databaseContext.Photos.AddRangeAsync(photos);
            await _databaseContext.SaveChangesAsync();
           
        }

        public void Update(Post post)
        {
            _databaseContext.Update(post);
        }
    }
}
