using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.DonationRepository
{
    public class DonationRepository : IDonationRepository
    {
        private readonly APIDbContext _appDBContext;

        public DonationRepository(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Donations>> GetDonations()
        {
            return await _appDBContext.Donations.ToListAsync();
        }

        public async Task<Donations> UpdateDonation(Donations objDonation)
        {
            _appDBContext.Entry(objDonation).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objDonation;
        }

        public bool DeleteDonation(int ID)
        {
            bool result = false;
            var donation = _appDBContext.Donations.Find(ID);
            if (donation != null)
            {
                _appDBContext.Entry(donation).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public async Task<Donations> GetDonationById(int ID)
        {
            return await _appDBContext.Donations.FindAsync(ID);
        }
       
    }
}
