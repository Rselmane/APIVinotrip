﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVinotrip.Models.EntityFramework;
using APIVinotrip.Models.DataManager;
using APIVinotrip.Models.Repository;

namespace APIVinotrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteDesVinsController : ControllerBase
    {
        private readonly IDataRepository<RouteDesVins> dataRepository;


        public RouteDesVinsController(IDataRepository<RouteDesVins> dataRepos)
        {
            dataRepository = dataRepos;
        }

        // GET: api/RouteDesVins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDesVins>>> GetRouteDesVins()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/RouteDesVins/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RouteDesVins>> GetRouteDesVinsById(int id)
        {
            var routeDesVins =  await dataRepository.GetById(id);

            if (routeDesVins == null)
            {
                return NotFound();
            }

            return  routeDesVins;
        }

        // GET: api/RouteDesVins/5
        [HttpGet]
        [Route("[action]/{title}")]
        [ActionName("GetRouteDesVinsByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RouteDesVins>> GetRouteDesVinsByTitle(string titre)
        {
            var routeDesVins =  await dataRepository.GetByString(titre);

            if (routeDesVins == null)
            {
                return NotFound();
            }

            return  routeDesVins;
        }

        // PUT: api/RouteDesVins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRouteDesVins(int id, RouteDesVins routeDesVins)
        {
            if (id != routeDesVins.IdRoute)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.Update(userToUpdate.Value, routeDesVins);
                return NoContent();
            }
        }

        // POST: api/RouteDesVins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RouteDesVins>> PostRouteDesVins(RouteDesVins routeDesVins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.Add(routeDesVins);

            return CreatedAtAction("GetById", new { id = routeDesVins.IdRoute }, routeDesVins); // GetById : nom de l’action
        }

        // DELETE: api/RouteDesVins/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRouteDesVins(int id)
        {
            var routeDesVins = await dataRepository.GetById(id);
            if (routeDesVins == null)
            {
                return NotFound();
            }
            await dataRepository.Delete(routeDesVins.Value);
            return NoContent();
        }

        //private bool RouteDesVinsExists(int id)
        //{
        //    return _context.RouteDesVins.Any(e => e.IdrouteDesVins == id);
        //}
    }
}
