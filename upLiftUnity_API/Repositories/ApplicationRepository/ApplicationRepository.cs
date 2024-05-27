using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using upLiftUnity_API.Services.EmailSender;


namespace upLiftUnity_API.Repositories.ApplicationRepository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly APIDbContext _appDBContext;
        private readonly IEmailSender _emailSender;

        public ApplicationRepository(APIDbContext context,IEmailSender emailSender)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
            _emailSender = emailSender ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<SupVol_Applications>> GetSupVol_Applications()
        {
            return await _appDBContext.SupVol_Applications.ToListAsync();
        }

        public async Task<SupVol_Applications> GetSupVol_ApplicationsById(int id)
        {
            return await _appDBContext.SupVol_Applications.FindAsync(id);
        }


        public async Task<SupVol_Applications> UpdateSupVol_Applications(int id, string status)
        {
            var application = await _appDBContext.SupVol_Applications.FindAsync(id);

            if (application == null)
            {
                return null;
            }

            application.ApplicationStatus = status;
            _appDBContext.Entry(application).State = EntityState.Modified;

            await _appDBContext.SaveChangesAsync();

            // Përcaktoni mesazhin e email-it bazuar në statusin e aplikimit
            string message = "";
            if (status == "pranohet")
            {
                message = "Përshëndetje " + application.NameSurname + ",\n\nAplikimi juaj për pozitën në linjën upLiftUnity është pranuar. Ju falënderojmë për interesimin dhe angazhimin tuaj. Ju lutemi të na kontaktoni për hollësi të mëtejshme rreth procedurës së aplikimit.\n\nMe respekt,\nEkipi i upLiftUnity";
            }
            else if (status == "refuzohet")
            {
                message = "Përshëndetje " + application.NameSurname + ",\n\nNa vjen keq, por aplikimi juaj për pozitën në linjën upLiftUnity është refuzuar. Ju falënderojmë për interesimin dhe përpjekjet tuaja. Ju inkurajojmë të vazhdoni të kërkoni mundësi të tjera dhe të mos heqni dorë nga synimet tuaja.\n\nMe respekt,\nEkipi i upLiftUnity";
            }
            else
            {
                message = "Përshëndetje " + application.NameSurname + ",\n\nJu njoftojmë që aplikimi juaj për pozitën në linjën upLiftUnity është në proces. Ju do të kontaktoheni në mënyrë të mëtejshme për hollësi rreth statusit të aplikimit.\n\nMe respekt,\nEkipi i upLiftUnity";
            }

            // Dërgoni email-in
            await _emailSender.SendEmailAsync(application.Email, "Njoftim rreth aplikimit në linjën upLiftUnity", message);

            return application;
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
