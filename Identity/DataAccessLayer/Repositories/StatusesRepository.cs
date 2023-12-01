using DataAccessLayer.Data;
using DataAccessLayer.Entities;


namespace DataAccessLayer.Repositories
{
    public class StatusesRepository:Repository<Status>
    {
        public StatusesRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {

        }
    }
}
