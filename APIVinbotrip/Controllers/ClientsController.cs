﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVinotrip.Models.EntityFramework;
using APIVinotrip.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using APIVinotrip.Helpers;

namespace APIVinotrip.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDataRepository<Client> dataRepository;

        public ClientsController(IDataRepository<Client> dataRepo)

        {
            dataRepository = dataRepo;
        }

        //////////////////////////////////
        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Policy = Policies.Client)]
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }
        [HttpGet]
        [Route("GetAdminData")]
        [Authorize(Policy = Policies.Dirigeant)]
        public IActionResult GetAdminData()
        {
            return Ok("This is a response from Admin method");
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Client/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var utilisateur = await dataRepository.GetById(id);
            //var utilisateur =  _context.Client.Find(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return  utilisateur;
        }

        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClientByEmail(string email)
        {
            var utilisateur =  await dataRepository.GetByString(email);
            //var utilisateur =  _context.Client.FirstOrDefault(e => e.Mail.ToUpper() == email.ToUpper());
            if (utilisateur == null)
            {
                return NotFound();
            }
            return  utilisateur;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.IdClient)
            {
                return BadRequest();
            }
            var userToUpdate = await  dataRepository.GetById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.Update(userToUpdate.Value, client);
                return NoContent();
            }
        }

        // POST: api/Clients
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            // Vérification si l'email existe déjà
            var existingClient = await dataRepository.GetAll();
            if (existingClient.Value.Any(c => c.EmailClient.ToUpper() == client.EmailClient.ToUpper()))
            {
                return Conflict(new { message = "Cet email est déjà utilisé." });
            }

            // Attribution du rôle Client par défaut
            client.IdRole = 1; // Rôle Client

            // Hacher le mot de passe avant de l'enregistrer
            string originalPassword = client.MdpClient; // Conserver le mot de passe original
            client.MdpClient = PasswordHasher.HashPassword(client.MdpClient);

            // Ne pas assigner le résultat à une variable
            await dataRepository.Add(client);

            // Vous devrez probablement récupérer l'ID du client d'une autre manière
            // Par exemple, si l'ID est généré par la base de données, il sera mis à jour dans l'objet client
            return CreatedAtAction(nameof(GetClientById), new { id = client.IdClient }, client);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var utilisateur =  await dataRepository.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
             await dataRepository.Delete(utilisateur.Value);
            return NoContent();
        }


        //private bool UtilisateurExists(int id)
        //{
        //    return _context.Client.Any(e => e.UtilisateurId == id);
        //}

    }
}
