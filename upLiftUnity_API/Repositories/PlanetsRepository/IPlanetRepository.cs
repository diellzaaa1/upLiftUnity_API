using upLiftUnity_API.DTOs.PlanetsDto;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.PlanetsRepository
{
    public interface IPlanetRepository
    {
        Task AddPlanet(PlanetDto planet);

        Task AddSatelite(SateliteDto sateliteDto);

        Task UpdatePlanet(string planetName, string newType);

        Task<List<SateliteDto>> GetPlanetSatelites(string planetName);

        Task DeleteSatelite(int sateliteId);

        Task<List<Planet>> GetPlanets();
        Task<List<Satelite>> GetSatelites();
    }
}
