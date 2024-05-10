using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.DTOs.DonationsDtos;
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


        public async Task<Dictionary<string, int>> GetMonthlyDonationCounts()
        {
            // Calculate date range for the past 12 months
            DateTime startDate = DateTime.Now.AddMonths(-12).Date;
            DateTime endDate = DateTime.Now.Date;

            var monthlyDonationCounts = await _appDBContext.Donations
                .Where(d => d.Date >= startDate && d.Date <= endDate)
                .GroupBy(d => new { Month = d.Date.Month, Year = d.Date.Year })
                .Select(g => new
                {
                    MonthYear = $"{g.Key.Month}/{g.Key.Year}",
                    Count = g.Count()
                })
                .ToDictionaryAsync(x => x.MonthYear, x => x.Count);

            return monthlyDonationCounts;
        }

        public async Task<List<MonthlyDonationDto>> GetDonationsPerMonth()
        {
            DateTime startDate = DateTime.Now.AddMonths(-12).Date;
            DateTime endDate = DateTime.Now.Date;

            var monthlyDonationCounts = await _appDBContext.Donations
               .Where(d => d.Date >= startDate && d.Date <= endDate)
               .GroupBy(d => new { Month = d.Date.Month, Year = d.Date.Year })
               .Select(g => new MonthlyDonationDto
               {
                   Month = $"{g.Key.Month}/{g.Key.Year}",
                   Donations = g.Count()
               }).ToListAsync();

            return monthlyDonationCounts;
        }
    }
}
