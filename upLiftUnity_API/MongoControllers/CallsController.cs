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
        [HttpGet("{id}")]
        public async Task<ActionResult<Call>> GetById(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            var filter = Builders<Call>.Filter.Eq(x => x.Id, objectId);
            var call = await _calls.Find(filter).FirstOrDefaultAsync();

            return call != null ? Ok(call) : NotFound();
        }



        [HttpPost]
        public async Task<ObjectId> Create(Call call)
        {
            await _calls.InsertOneAsync(call);
            return (ObjectId)call.Id;
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Call call)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            // Set the Id property of the call object to the parsed ObjectId
            call.Id = objectId;

            var filter = Builders<Call>.Filter.Eq(x => x.Id, objectId);

            var update = Builders<Call>.Update
                .Set(x => x.CallerNickname, call.CallerNickname)
                .Set(x => x.Description, call.Description)
                .Set(x => x.RiskLevel, call.RiskLevel);

            var result = await _calls.UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                return Ok(call);
            }
            else
            {
                return NotFound();
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            var filter = Builders<Call>.Filter.Eq(x => x.Id, objectId);
            await _calls.DeleteOneAsync(filter);
            return Ok();
        }


    }
}
