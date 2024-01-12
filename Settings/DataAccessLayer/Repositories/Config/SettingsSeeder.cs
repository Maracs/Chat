using DataAccessLayer.Entities;
using DataAccessLayer.Seeders;

namespace DataAccessLayer.Repositories.Config
{
    public class SettingsSeeder : IMongoSeeder<SettingsInfo>
    {
        public IEnumerable<SettingsInfo> Seed()
        {
            var projectReports = new List<SettingsInfo>();
            return projectReports;
        }
    }
}
