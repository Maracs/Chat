namespace DataAccessLayer.Contracts
{
    public interface IMongoRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
