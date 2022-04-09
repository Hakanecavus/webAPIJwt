#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teemUpAPIv2.Data;
using teemUpAPIv2.Models;

using teemUpAPIv2.Services.UserServices;

namespace teemUpAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserDetailsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public UserDetailsController(IUserService userService, DataContext context)
        {
            _userService = userService;
            _context = context;
        }

        // GET: api/UserDetails
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UserDetail>>> GetuserDetails()
        //{
        //    return await _context.userDetails.ToListAsync();
        //}

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUserDetail(string id)
        {
            var userDetail = await _context.userDetails.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return userDetail;
        }

        // PUT: api/UserDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail(string id, UserDetail userDetail)
        {
            if (id != userDetail.email)
            {
                return BadRequest();
            }

            if(id != _userService.GetMyName())
            {
                return Unauthorized();
            }
            _context.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
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

        // POST: api/UserDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail(UserDetail userDetail)
        {

            if (UserDetailExists(userDetail.email))
            {
                return BadRequest();
            }
            if (userDetail.email != _userService.GetMyName())
            {
                return Unauthorized();
            }

            if (userDetail.email != userDetail.userId)
            {
                return BadRequest();
            }

            _context.userDetails.Add(userDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserDetailExists(userDetail.email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserDetail", new { id = userDetail.email }, userDetail);
        }

        // DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(string id)
        {
            if (id != _userService.GetMyName())
            {
                return Unauthorized();
            }
            
            var userDetail = await _context.userDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            if (userDetail.email != userDetail.userId)
            {
                return BadRequest();
            }

            _context.userDetails.Remove(userDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDetailExists(string id)
        {
            return _context.userDetails.Any(e => e.email == id);
        }
    }
}
