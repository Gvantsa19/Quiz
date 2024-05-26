namespace FlatRockProject.Application.Services.Author
{
    public interface IAuthorService
    {
        Task<Infrastructure.Entities.Author> GetRandomAuthorOutsideRange(params long[] usedIds);
    }
}
