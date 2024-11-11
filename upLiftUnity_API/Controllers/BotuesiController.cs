using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotuesiController : ControllerBase
    {
        private readonly APIDbContext _context;

        public BotuesiController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Botuesi>>> GetBotuesit()
        {
            return await _context.Botuesit.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBotuesi(int id)
        {
            var botuesi = await _context.Botuesit.FindAsync(id);
            if (botuesi == null)
            {
                return NotFound();
            }

            return Ok(botuesi);
        }

        [HttpPost]
        public async Task<ActionResult<Botuesi>> CreateBotuesi(Botuesi botuesi)
        {
            _context.Botuesit.Add(botuesi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBotuesi), new { id = botuesi.Id }, botuesi);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBotuesi(int id, Botuesi botuesi)
        {
            if (id != botuesi.Id)
            {
                return BadRequest();
            }

            _context.Entry(botuesi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Botuesit.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBotuesi(int id)
        {
            var botuesi = await _context.Botuesit.FindAsync(id);
            if (botuesi == null)
            {
                return NotFound();
            }
            
            _context.Botuesit.Remove(botuesi);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
    }


}

