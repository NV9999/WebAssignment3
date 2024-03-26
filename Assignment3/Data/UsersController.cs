using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Data
{
    public class UsersController : Controller
    {
        private readonly WebProjectContext _context;

        public UsersController(WebProjectContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<JsonResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return Json(users);
        }

        // GET: Users/Details/5
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
            {
                return Json(new { error = "Id parameter is missing" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return Json(new { error = "User not found" });
            }

            return Json(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "User created successfully", user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id parameter is missing");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return View(user);
        }

        // PUT: Users/Edit/5
        // PUT: Users/Edit/5
        // PUT: Users/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID in the URL does not match the ID in the request body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }

                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Username = user.Username;
                existingUser.PurchaseHistory = user.PurchaseHistory;
                existingUser.ShippingAddress = user.ShippingAddress;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "User updated successfully", user = existingUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id parameter is missing");
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
