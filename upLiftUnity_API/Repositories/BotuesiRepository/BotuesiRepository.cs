using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.BotuesiRepository
{
    public class BotuesiRepository : IBotuesiRepository
    {
        private readonly APIDbContext _dbContext;

        public BotuesiRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateBotuesi(string publisherName, string location)
        {
            var botuesi = new Botuesi()
            {
                PublisherName = publisherName,
                Location = location
            };

            await _dbContext.Botuesit.AddAsync(botuesi);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Botuesi>> GetAllBotuesit()
        {
            return await _dbContext.Botuesit.ToListAsync();
        }

        public async Task<Botuesi> GetBotuesiById(int id)
        {
            return await _dbContext.Botuesit.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateBotuesi(int id,string publisherName,string location)
        {
            var botuesiInDb = await _dbContext.Botuesit.FirstOrDefaultAsync(x => x.Id == id);
            if(botuesiInDb == null)
            {
                throw new InvalidOperationException("botuesi was not found!");
            }
            botuesiInDb.PublisherName = publisherName;
            botuesiInDb.Location = location;

            await _dbContext.SaveChangesAsync();
        }
    }
}
