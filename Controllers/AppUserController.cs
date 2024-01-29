using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using LSS.Dtos;
using LSS.Model;
using LSS.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSS.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AddUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AddUserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("login/")]
        public IActionResult login(LoginDto loginDto)
        {
            AppUser user = _context.GetByEmail(loginDto.email);

            if (user == null) return BadRequest("Invalid Credentials");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.password, user.password)) return BadRequest("Incorrect Password");

            return Ok(user);
        }

        [HttpPost("adduser/")]
        public IActionResult addUser(RegisterDto registerDto)
        {
            AppUser user = new AppUser();
            user.user_name = registerDto.u_name;
            user.email = registerDto.email;
            user.role = registerDto.role;
            int length = 10;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            Console.WriteLine(randomString);
            user.password = BCrypt.Net.BCrypt.HashPassword(randomString);

            _context.Users.Add(user);
            if (_context.SaveChanges() == 409) return BadRequest("User not added");
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet("users/")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}