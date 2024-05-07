using upLiftUnity_API.DTOs.DonationsDtos;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.DonationRepository
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donations>> GetDonations();

        Task<Donations> GetDonationById(int id);

        Task <Donations> UpdateDonation (Donations objDonation);

        bool DeleteDonation(int id);

        Task<Dictionary<string, int>> GetMonthlyDonationCounts();

        Task<List<MonthlyDonationDto>> GetDonationsPerMonth();
    }
}
