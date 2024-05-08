using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpPut]
        public async Task<IActionResult> Update(Call call)
        {
            var filter = Builders<Call>.Filter.Eq(x => x.Id, call.Id);

            await _calls.ReplaceOneAsync(filter, call);
            return Ok(call);
        }

        [HttpDelete]
        public async Task<ActionResult>Delete(ObjectId id)
        {
            var filter = Builders<Call>.Filter.Eq(x => x.Id, id);
            await _calls.DeleteOneAsync(filter);
            return Ok();
        }
       
    }
}
