using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ApplicationRepository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly APIDbContext _appDBContext;

        public ApplicationRepository(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<SupVol_Applications>> GetSupVol_Applications()
        {
            return await _appDBContext.SupVol_Applications.ToListAsync();
        }

        public async Task<SupVol_Applications> GetSupVol_ApplicationsById(int id)
        {
            return await _appDBContext.SupVol_Applications.FindAsync(id);
        }

        public async Task<SupVol_Applications> UpdateSupVol_Applications(int id)
        {
            var application = await _appDBContext.SupVol_Applications.FindAsync(id);

            if (application == null)
            {
                return null; 
            }

            application.ApplicationStatus = "E Shqyrtuar";
            _appDBContext.Entry(application).State = EntityState.Modified;

            await _appDBContext.SaveChangesAsync(); // Përditësoni bazën e të dhënave

            return application; // Kthe aplikacionin e përditësuar
        }


        public bool DeleteApplication(int Id)
        {
            bool result = false;
            var app = _appDBContext.SupVol_Applications.Find(Id);
            if (app != null)
            {
                _appDBContext.Entry(app).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;


            }
            else {
                result = false;
            }

            return result;

        }

        public async Task<SupVol_Applications> InsertSupVol_Application(SupVol_Applications objApp)
        {
            _appDBContext.SupVol_Applications.Add(objApp);
            await _appDBContext.SaveChangesAsync();
            return objApp;
        }

        public async Task<IEnumerable<SupVol_Applications>> GetApplicationsByType(string type)
        {
            return await _appDBContext.SupVol_Applications.Where(application => application.ApplicationType == type).ToListAsync();
        }
    }
}
