using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateForum.Context;
using PrivateForum.Entities;

namespace PrivateForum.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Invites")]
    [Authorize]
    public class InvitesController : Controller
    {
        private readonly ApplicationContext _context;
        private UserManager<ApplicationUser> _userManager;

        public InvitesController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
             _userManager = userManager;
        }

        // GET: api/Invites
        [HttpGet]
        public IEnumerable<Invite> GetInvites()
        {
            return _context.Invites.Include(i => i.User).Where(i => i.User.Id == _userManager.GetUserAsync(HttpContext.User).Result.Id);
        }

        // GET: api/Invites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invite = await _context.Invites.SingleOrDefaultAsync(m => m.Id == id);

            if (invite == null)
            {
                return NotFound();
            }

            return Ok(invite);
        }

        [HttpPost]
        public IActionResult PostInvite()
        {
            ApplicationUser user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.InviteCount > 0)
            {
                user.InviteCount--;
                _context.SaveChanges();
                Invite invite = new Invite(user);
                _context.Invites.Add(invite);
                _context.SaveChanges();
                return CreatedAtAction("GetInvite", new { id = invite.Id }, invite);
            }
            return NotFound();
        }


        private bool InviteExists(int id)
        {
            return _context.Invites.Any(e => e.Id == id);
        }
    }
}