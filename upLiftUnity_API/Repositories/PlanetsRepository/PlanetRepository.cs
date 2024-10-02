using upLiftUnity_API.DTOs.PlanetsDto;
using upLiftUnity_API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace upLiftUnity_API.Repositories.PlanetsRepository
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly APIDbContext _dbContext;


        public PlanetRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPlanet(PlanetDto planet)
        {
            _dbContext.Planets.Add(new Planet()
            {
                Name = planet.Name,
                Type = planet.Type,
                IsDeleted = false
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSatelite(SateliteDto sateliteDto)
        {
            _dbContext.Satelites.Add(new Satelite()
            {
                PlanetId = sateliteDto.PlanetId,
                Name = sateliteDto.Name,
                IsDeleted = false
            });

            await _dbContext.SaveChangesAsync();


        }

        public async Task DeleteSatelite(int sateliteId)
        {
            var satelite = await _dbContext.Satelites.FirstOrDefaultAsync(x => x.Id == sateliteId);
            if (satelite != null)
            {
                satelite.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Could not find the satelite with id: " + sateliteId);
            }
        }

        public async Task<List<Planet>> GetPlanets()
        {
            var planets = await _dbContext.Planets
                                          .Where(x => !x.IsDeleted)
                                          .ToListAsync();

            return planets;
        }

        public async Task<List<SateliteDto>> GetPlanetSatelites(string planetName)
        {
            var planet = await _dbContext.Planets
                .Include(x => x.Satelites
                        .Where(s => !s.IsDeleted)
                )
                .FirstOrDefaultAsync(planet => planet.Name == planetName);

            if (planet == null)
            {
                throw new InvalidOperationException("Could not find the planet with name: " + planetName);
            }

            var satelites = planet.Satelites;

            var satelitesDto = planet.Satelites.Select(satelite => new SateliteDto()
            {
                Name = satelite.Name,
                IsDeleted = satelite.IsDeleted,
                PlanetId = satelite.PlanetId
            });
            return satelitesDto.ToList();
        }

        public async Task<List<Satelite>> GetSatelites()
        {
            var satelites = await _dbContext.Satelites
                                           .Where(x => !x.IsDeleted)
                                           .ToListAsync();

            return satelites;
        }

        public async Task UpdatePlanet(string planetName, string newType)
        {
            var planet = await _dbContext.Planets.FirstOrDefaultAsync(x => x.Name == planetName);

            if (planet == null)
            {
                throw new InvalidOperationException("Could not find the planet with name: " + planetName);
            }

            planet.Type = newType;

            await _dbContext.SaveChangesAsync();
        }
    }
}
