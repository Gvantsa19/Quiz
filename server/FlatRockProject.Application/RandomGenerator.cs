using FlatRockProject.Application.Services.Author;


namespace FlatRockProject.Application
{
    public class RandomGenerator : IRandomGenerator
    {
        private static readonly Random GetRandom = new Random();

        public long GetRandomNumber(long min, long max) => GetRandom.NextInt64(min, max);
    }
}
