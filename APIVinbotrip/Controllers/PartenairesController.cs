﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVinotrip.Models.EntityFramework;

namespace APIVinotrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartenairesController : ControllerBase
    {
        private readonly DBVinotripContext _context;

        public PartenairesController(DBVinotripContext context)
        {
            _context = context;
        }

        // GET: api/Partenaires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partenaire>>> GetPartenaires()
        {
            return await _context.Partenaires.ToListAsync();
        }

        // GET: api/Partenaires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partenaire>> GetPartenaire(int id)
        {
            var partenaire = await _context.Partenaires.FindAsync(id);

            if (partenaire == null)
            {
                return NotFound();
            }

            return partenaire;
        }

        // PUT: api/Partenaires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartenaire(int id, Partenaire partenaire)
        {
            if (id != partenaire.IdPartenaire)
            {
                return BadRequest();
            }

            _context.Entry(partenaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartenaireExists(id))
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

        // POST: api/Partenaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partenaire>> PostPartenaire(Partenaire partenaire)
        {
            _context.Partenaires.Add(partenaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartenaire", new { id = partenaire.IdPartenaire }, partenaire);
        }

        // DELETE: api/Partenaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartenaire(int id)
        {
            var partenaire = await _context.Partenaires.FindAsync(id);
            if (partenaire == null)
            {
                return NotFound();
            }

            _context.Partenaires.Remove(partenaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartenaireExists(int id)
        {
            return _context.Partenaires.Any(e => e.IdPartenaire == id);
        }
    }
}
