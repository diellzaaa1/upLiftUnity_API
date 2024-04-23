using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.DonationRepository;
using upLiftUnity_API.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization; // Add this namespace for Task
using System.Linq; // Add this namespace for Any method
using Stripe.Checkout;


namespace upLiftUnity_API.Controllers
{
    [Route("api/donations")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly IDonationRepository _donation;
        private readonly ILogger<DonationController> _logger;
        public DonationController(APIDbContext _dbcontext, IDonationRepository _dbDonations, ILogger<DonationController> logger)
        {
            _context = _dbcontext;
            _donation = _dbDonations;
            _logger = logger;
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

        //[HttpPost]
        //[Route("SaveDonation")]

        //public IActionResult CreateDonation([FromBody] Donations donation)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (_context.Donations.Any(d => d.DonationID == donation.DonationID))
        //    {
        //        return Conflict("Ky donacion eshte realizuar nje here!");
        //    }
        //    _context.Donations.Add(donation);
        //    _context.SaveChanges();

        //    return Ok();
        //}



        [HttpPost]
        [Route("CreateCheckoutSession/{packageId}")]
        public async Task<IActionResult> CreateCheckoutSession(int packageId)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51NAuXrA98ZUN1q4APNCsEp07wxgzIxq8DwwO0PZSiScgHaLrQdErKlj0ZRE60EqjIo46fLmUdJgMtu8rhOOrrjNW00RqbhPzCQ";

                // Determine the package details based on the packageId received from the front end
                string packageName;
                int unitAmount;

                switch (packageId)
                {
                    case 1:
                        packageName = "Package1";
                        unitAmount = 10000;
                        break;
                    case 2:
                        packageName = "Package2";
                        unitAmount = 20000;
                        break;
                    case 3:
                        packageName = "Package3";
                        unitAmount = 30000;
                        break;
                    default:
                        return BadRequest("Invalid package ID");
                }

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "eur",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = packageName,
                                },
                                UnitAmount = unitAmount,
                            },
                            Quantity = 1,
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = "http://localhost:5051/api/donations/webhook?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "http://localhost:5051/api/donations/failedWebook",
                  
                  
                };

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new { url = session.Url });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating checkout session: " + ex.Message);
                return StatusCode(500, "Error creating checkout session");
            }
        }

        [HttpPost]
        [Route("failedWebhook")]
        public IActionResult FailedWebhook()
        {
            _logger.LogError($"Failed {nameof(FailedWebhook)}");
            return StatusCode(500, "Error creating webhook");
        }

        //the command in cmd to listen for stripe webhook
        //stripe listen --forward-to http://localhost:5051/api/donations/webhook


        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> Index()
        {

            try
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                var stripeSign = Request.Headers["Stripe-Signature"];

                var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                "whsec_52eed3d8644e8766635d83251bc6f88c065b74c3aa42beeda97870cefb074d4f",
                throwOnApiVersionMismatch: false,
                tolerance: 500
                );

                
                switch (stripeEvent.Type)
                {
                    case Events.CheckoutSessionCompleted:

                        var paymentIntent = stripeEvent.Data.Object as Session;
         
                        try
                        {
                            var donation = new Donations
                            {   

                                NameSurname = paymentIntent.CustomerDetails?.Name,
                                Email = paymentIntent.CustomerDetails?.Email,
                                Amount = int.Parse(paymentIntent.AmountSubtotal.ToString()),
                                TransactionId = paymentIntent.Id,
                                Date = DateTime.Now
                            };

                            if (!ModelState.IsValid)
                            {
                                return BadRequest(ModelState);
                            }

                            if (_context.Donations.Any(d => d.DonationID == donation.DonationID))
                            {
                                return Conflict("Ky donacion eshte realizuar nje here!");
                            }
                            _context.Donations.Add(donation);
                            _context.SaveChanges();

                            return Ok("Donacioni është ruajtur me sukses!");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error saving donation to database");

                            return StatusCode(500, "Error saving donation to database");
                        }
                    case "payment_intent.created":
                        var payment = stripeEvent.Data.Object as PaymentIntent;
                        Console.WriteLine(payment);
                        break;

                    default:
                        Console.WriteLine("Unhandled events:", stripeEvent.Type);
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing Stripe webhook");
                return BadRequest();
            }

            return Ok();
        }
    }
}
