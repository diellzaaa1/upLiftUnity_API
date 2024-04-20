using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.DonationRepository;
using upLiftUnity_API.Services; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Add this namespace for Task

namespace upLiftUnity_API.Controllers
{
    [Route("api/donations")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly IDonationRepository _donation;

        public DonationController(APIDbContext _dbcontext,IDonationRepository _dbDonations)
        {
            _context = _dbcontext;
            _donation = _dbDonations;
        }

        [HttpGet]
        [Route("GetDonations")]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Get()
        {
            var donations = await _donation.GetDonations();
            return Ok(donations);
        }

        [HttpPut]
        [Route("UpdateDonation")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Put(Donations donation)
        {
            await _donation.UpdateDonation(donation);
            return Ok("Updated Successfully");
        }

        [HttpDelete]
        [Route("DeleteDonation")]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Delete(int id)
        {
            _donation.DeleteDonation(id);
            return new JsonResult("Deleted Successfully");
        }

        [HttpGet]
        [Route("GetDonationByID/{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetDonationByID(int Id)
        {
            return Ok(await _donation.GetDonationById(Id));
        }

        [HttpPost]
        [Route("SaveDonation")]

        public IActionResult CreateDonation([FromBody] Donations donation)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if(_context.Donations.Any( d => d.DonationID == donation.DonationID ))
            {
                return Conflict("Ky donacion eshte realizuar nje here!");
            }
            _context.Donations.Add(donation);
            _context.SaveChanges();

            return Ok();
        }
    


    }
}
