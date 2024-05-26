using FlatRockProject.Infrastructure.Persistence;

namespace FlatRockProject.Application.Services.Abstract
{
    public abstract class BaseService
    {
        protected BaseService(FRPDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected FRPDbContext DbContext { get; }
    }
}
