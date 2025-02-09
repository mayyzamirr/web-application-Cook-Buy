﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebProject.Models;

namespace WebApplication.Controllers
{

    public class UsersController : Controller
    {
        private readonly WebApplicationContext _context;

        public UsersController(WebApplicationContext context)
        {
            _context = context;
        }

        // GET: Users/SignUp
        public IActionResult SignUp()
        {
            ViewData["Branches"] = new SelectList(_context.Branches, "BranchId", "Name");
            return View();
        }
        //Get: Users/SignIn
        public IActionResult SignIn()
        {
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("UserId,Username,Password,Email,RepeatPassword,CreditCard,Branches,BranchId")] Users users)
        {
            if (ModelState.IsValid)
            {
                var q = _context.Users.FirstOrDefault(u => u.Username == users.Username);
                var d = _context.Users.FirstOrDefault(u => u.Email == users.Email);
               
                if (q == null && d == null)
                {
                    if (users.Password == users.RepeatPassword)
                    {
                        _context.Add(users);
                        await _context.SaveChangesAsync();

                        var u = _context.Users.FirstOrDefault(u => u.Username == users.Username && u.Password == users.Password);

                        Signin(u);
                        return RedirectToAction("Create", "CreditCards");

                    }
                    else ViewData["Error"] = "Password Invalid";
                }
                else
                    ViewData["Error"] = "Unable to register, try another user name or email";
            }
            return View(users);

        }

        // POST: Users/SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn([Bind("Username,Password,Email, UserId,Branches,BranchId")] Users users)
        {
            if (ModelState.IsValid)
            {
                var q = _context.Users.FirstOrDefault(u => u.Username == users.Username && u.Password == users.Password);
                if (q != null)
                {
                    HttpContext.Session.SetString("username", q.Username);

                    Signin(q);
                    if (Paid(users) == true)
                    {
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        return RedirectToAction("Create", "CreditCards");
                    }
                }
                else
                    ViewData["Error"] = "Unable to Login, try another user name or password";
            }
            return View(users);
        }

        private async void Signin(Users account)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, account.Username), new Claim(ClaimTypes.Role, account.Type.ToString()), };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)}; }
                
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("SignIn");
        }

        public Boolean Paid(Users users)
        {
            var q = _context.CreditCard.FirstOrDefault(u => u.UserName == users.Username);
            
            if (q == null)
            {
                return false;
            }

            return true;
        }

    }
            
}




















/*
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password,Email")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
*/