using upLiftUnity_API.Models;
using Microsoft.EntityFrameworkCore;

namespace upLiftUnity_API.Repositories.PlanetRepository
{
    public class PlanetRepo : IPlanetRepo
    {
        private readonly APIDbContext _appDBContext;

        public PlanetRepo(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Planet> CreatePlanet()
        {
            _appDBContext.Planet.Add(planet);
            await _appDBContext.SaveChangesAsync();
            return planet;
        }

        public async Task<IEnumerable<Planet>> GetPlanets()
        {
            var planets = await _appDBContext.Planet.Where(p=> !p.IsDeleted).ToListAsync();

            return planets;
            
        }
        public async Task<Planet> GetPlanetId(int planetId)
        {
            return await _appDBContext.Planet.FindAsync(planetId);
        }

        public async Task<Planet> GetPlanetByName(string name)
        {
            return await _appDBContext.Planet.FindAsync(name);
        }

        public async Task<bool> UpdatePlanet(string name, string type)
        {
            var planet = await _appDBContext.Planet.FirstOrDefaultAsync(p => p.Name == name);

            if (planet == null)
                return false;

            planet.Type = type; 

            await _appDBContext.SaveChangesAsync(); 

            return true;
        }

        public async Task<bool> DeletePlanet(int planetId)
        {
            var planet = await _appDBContext.Planet.FindAsync(planetId);
            if (planet == null)
                return false;

            planet.IsDeleted = true; 

            await _appDBContext.SaveChangesAsync(); 

            return true;
        }
    }
}
