using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using upLiftUnity_API.MongoModels;

namespace upLiftUnity_API.MongoControllers
{
    [Route("/calls")]
    [ApiController]
    public class CallsController : Controller
    {
        private readonly IMongoCollection<Call> _calls;
        public CallsController(MongoDbContext mongoDbContext) {
            _calls = mongoDbContext.Database?.GetCollection<Call>("call");
        }

        [HttpGet]
       public async Task<IEnumerable<Call>> Get()
        {
            return await _calls.Find(FilterDefinition<Call>.Empty).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Call call)
        {
            await _calls.InsertOneAsync(call);
            return Ok(call);
        }

       
    }
}
