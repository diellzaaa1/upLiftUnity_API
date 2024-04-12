using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ApplicationRepository
{
    public interface IApplicationRepository
    {
        Task<SupVol_Applications> InsertSupVol_Application(SupVol_Applications objApp);

        Task<IEnumerable<SupVol_Applications>> GetSupVol_Applications();

        Task<SupVol_Applications> GetSupVol_ApplicationsById(int id);

       Task <IEnumerable<SupVol_Applications>> GetApplicationsByType(String type);

        Task<SupVol_Applications> UpdateSupVol_Applications(SupVol_Applications objApp);

        bool DeleteApplication(int Id);

        

    }
}
