using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.PlanetRepository
{
    public interface IPlanetRepo
    {
        Task<IEnumerable<Planet>> GetPlanets();

        Task<Planet> GetPlanetId(int planetId);

        Task<Planet> GetPlanetByName(string name);

        Task<bool> UpdatePlanet(string name, string type);

        Task<bool> DeletePlanet(int planetId);

        Task<Planet> CreatePlanet(Planet planet);



    }
}
