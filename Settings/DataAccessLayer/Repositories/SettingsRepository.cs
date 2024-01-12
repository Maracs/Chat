using DataAccessLayer.Entities;
using DataAccessLayer.Extensions;
using DataAccessLayer.Seeders;
using Microsoft.Extensions.Options;
using Shared.Repository.NoSql;

namespace DataAccessLayer.Repositories
{
    public class SettingsRepository :MongoRepository<SettingsInfo>
    {
        public SettingsRepository(
            IOptions<MongoDbSettings<SettingsInfo>> mongoSettings,
            IMongoSeeder<SettingsInfo> mongoDbSeeder
        )
            : base(mongoSettings, mongoDbSeeder) { }
    }
}
