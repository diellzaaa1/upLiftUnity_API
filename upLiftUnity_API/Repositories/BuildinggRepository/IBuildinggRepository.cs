
using upLiftUnity_API.DTOs.BuildingDtos;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.BuildinggRepository
{
    public interface IBuildinggRepository
    {

        Task AddBuilding(BuildingDto buildingDto);

        Task AddRenovation(RenovationDto renovationDto);

        Task DeleteRenovation(int renovationId);

        //Task<List<RenovationDto>> GetRenovationOfCity(string location);

        Task<IEnumerable<Buildingg>> GetBuildinggs();
        Task<IEnumerable<Renovationn>> GetRenovationns();
    }
}
