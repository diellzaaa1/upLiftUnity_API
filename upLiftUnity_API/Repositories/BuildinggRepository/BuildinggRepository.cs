using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.DTOs.BuildingDtos;
using upLiftUnity_API.DTOs.PlanetsDto;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.BuildinggRepository
{
    public class BuildinggRepository : IBuildinggRepository
    {
        private readonly APIDbContext _dbContext;
        public BuildinggRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddBuilding(BuildingDto buildingDto)
        {
            _dbContext.Buildinggs.Add(new Buildingg()
            {
                Name = buildingDto.Name,
                Location = buildingDto.Location,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRenovation(RenovationDto renovationDto)
        {
            _dbContext.Renovationns.Add(new Renovationn()
            {
                Description = renovationDto.Description,
                Cost = renovationDto.Cost,
                BuildingId = renovationDto.BuildingId
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRenovation(int renovationId)
        {
            var renovation = await _dbContext.Renovationns.FirstOrDefaultAsync(x => x.Id == renovationId);
            if (renovation != null)
            {
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Could not find the renovation with id: " + renovationId);
            }
        }

        public async Task<IEnumerable<Buildingg>> GetBuildinggs()
        {
            return await _dbContext.Buildinggs.ToListAsync();
        }

        public async Task<IEnumerable<Renovationn>> GetRenovationns()
        {
            return await _dbContext.Renovationns.ToListAsync();
        }

        //public async Task<List<RenovationDto>> GetRenovationOfCity(string location)
        //{
        //    var renovation = await _dbContext.Renovationns
        //        .Include(x => x.Buildingg)
        //         .FirstOrDefaultAsync(renovation => renovation.L == location);

        //    if (planet == null)
        //    {
        //        throw new InvalidOperationException("Could not find the planet with name: " + planetName);
        //    }

        //    var satelites = planet.Satelites;

        //    var satelitesDto = planet.Satelites.Select(satelite => new SateliteDto()
        //    {
        //        Name = satelite.Name,
        //        IsDeleted = satelite.IsDeleted,
        //        PlanetId = satelite.PlanetId
        //    });
        //    return satelitesDto.ToList();
        //}
   // }
    }
}
