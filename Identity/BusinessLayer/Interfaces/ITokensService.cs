using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ITokensService
    {
        public Task<string> GetTokenAsync(User user);
    }
}
