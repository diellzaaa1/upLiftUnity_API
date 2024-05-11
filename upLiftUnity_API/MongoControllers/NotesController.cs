using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using upLiftUnity_API.MongoModels;

namespace upLiftUnity_API.MongoControllers
{
    [Route("/notes")]
    [ApiController]
    public class NotesController : Controller
    {
        private readonly IMongoCollection<Notes> _notes;

        public NotesController(MongoDbContext mongoDbContext)
        {
            _notes = mongoDbContext.Database?.GetCollection<Notes>("notes");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Notes note)
        {
            try
            {
                note.CreatedAt = DateTime.UtcNow;
                note.UpdatedAt = DateTime.UtcNow;
                note.NoteId = Guid.NewGuid();

                await _notes.InsertOneAsync(note);

                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotes(int userId)
        {
            try
            {
                var notes = await _notes.Find(n => n.UserId == userId).ToListAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var notes = await _notes.Find(_ => true).ToListAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Notes updatedNote)
        {
            try
            {
                using var cursor = await _notes.FindAsync(x => x.NoteId == Guid.Parse(id));
                var firstNote = await cursor.FirstAsync();

                updatedNote.Id = firstNote.Id;

                var result = await _notes.ReplaceOneAsync(x => x.NoteId == Guid.Parse(id), updatedNote);

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
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            try
            {
                var noteId = Guid.Parse(id);

                var note = await _notes.Find(x => x.NoteId == noteId).FirstOrDefaultAsync();

                if (note == null)
                {
                    return NotFound();
                }

           
                var result = await _notes.DeleteOneAsync(x => x.NoteId == noteId);

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
   
