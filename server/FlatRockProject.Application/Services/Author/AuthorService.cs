using FlatRockProject.Application.Services.Abstract;
using FlatRockProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlatRockProject.Application.Services.Author
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly FRPDbContext _context;
        private readonly IRandomGenerator _randomGenerator;

        public AuthorService(FRPDbContext context, IRandomGenerator randomGenerator) : base(context)
        {
            _context = context;
            _randomGenerator = randomGenerator;
        }
        public async Task<Infrastructure.Entities.Author> GetRandomAuthorOutsideRange(params long[] usedIds)
        {
            var availableIds = await _context.Authors
                                           .Where(author => !usedIds.Contains(author.Id))
                                           .Select(author => author.Id)
                                           .ToArrayAsync();

            if (availableIds.Length == 0)
            {
                return null;
            }

            var randomAvailableNumber = _randomGenerator.GetRandomNumber(0, availableIds.Length);
            var randomAuthorId = availableIds[randomAvailableNumber];

            return await _context.Authors.FindAsync(randomAuthorId);
        }
    }
    public interface IRandomGenerator
    {
        long GetRandomNumber(long min, long max);
    }
}
