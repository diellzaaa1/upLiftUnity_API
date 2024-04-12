using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.DonationRepository
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donations>> GetDonations();

        Task<Donations> GetDonationById(int id);

        Task <Donations> UpdateDonation (Donations objDonation);

        bool DeleteDonation(int id);
    }
}
