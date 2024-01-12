namespace DataAccessLayer.Seeders
{
    public interface IMongoSeeder<TEntity>
    {
        IEnumerable<TEntity> Seed();
    }
}
