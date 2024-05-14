using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using upLiftUnity_API.MongoModels;

namespace upLiftUnity_API.MongoControllers
{
    [Route("/calls")]
    [ApiController]
    public class CallsController : Controller
    {
        private readonly IMongoCollection<Call> _calls;
        public CallsController(MongoDbContext mongoDbContext)
        {
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
        public async Task<IActionResult> Create([FromBody] Call call)
        {
            call.CallId = Guid.NewGuid();
            await _calls.InsertOneAsync(call);
            return Ok(call);
        }



        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Call call)
        {
            try
            {
                using var cursor = await _calls.FindAsync(x => x.CallId == Guid.Parse(id));
                var firstCall = await cursor.FirstAsync();

                call.Id = firstCall.Id;

                var result = await _calls.ReplaceOneAsync(x => x.CallId == Guid.Parse(id), call);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Update failed");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error ocurred: {ex.Message}");
            }
        }






        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (!ObjectId.TryParse(id, out ObjectId objectId))
        //    {
        //        return BadRequest("Invalid ObjectId format.");
        //    }

        //    var filter = Builders<Call>.Filter.Eq(x => x.Id, objectId);
        //    await _calls.DeleteOneAsync(filter);
        //    return Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCall(string id)
        {
            try
            {
                var callId = Guid.Parse(id);

                var note = await _calls.Find(x => x.CallId == callId).FirstOrDefaultAsync();

                if (note == null)
                {
                    return NotFound();
                }


                var result = await _calls.DeleteOneAsync(x => x.CallId == callId);

                if (result.DeletedCount == 0)
                {
                    return StatusCode(500, "Failed to delete note");
                }

                return Ok("Note deleted successfully.");
            }
            catch (FormatException)
            {
                return BadRequest("Invalid ID format");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
    




