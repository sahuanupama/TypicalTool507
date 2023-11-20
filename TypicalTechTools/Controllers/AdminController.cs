using TypicalTechTools.Models;
using Microsoft.AspNetCore.Mvc;
using TypicalTechTools.DataAccess;

namespace TypicalTools.Controllers
    {
    public class AdminController : Controller
        {
        private readonly TypicalToolDBContext _context;

        public AdminController(TypicalToolDBContext context)
            {
            _context = context;
            }

        public IActionResult Login()
            {

            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUserDTO user)
            {
            var account = _context.AdminUsers.Where(a => a.UserName == user.UserName).FirstOrDefault();
            if (account == null)
                {
                ViewBag.LoginError = "Username or Password incorrect";
                return View(user);
                }

            if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, account.Password))
                {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetInt32("userId", account.Id);
                return RedirectToAction("Index", "Product");
                }

            ViewBag.LoginError = "Username or Password incorrect";
            return View(user);
            }

        public IActionResult LogOff()
            {
            HttpContext.Session.SetString("IsAuthenticated", "false");
            return RedirectToAction("Index", "Product");
            }

        public IActionResult CreateUser()
            {
            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(CreateUserDTO user)
            {

            string authenticated = HttpContext.Session.GetString("IsAuthenticated") ?? "false";
            if (authenticated.Equals("false"))
                {
                ViewBag.LoginError = "Login required to access this page.";
                return RedirectToAction("Login");
                }

            //Check that the username and confirmation are the same
            if (user.Password.Equals(user.PasswordConfirmation) == false)
                {
                //Create message and return if not matching
                ViewBag.CreateUserError = "Password and Password confirmation do not match";
                return View(user);
                }
            //Check if the username is already taken
            if (_context.AdminUsers.Any(a => a.UserName == user.UserName))
                {
                //Create message and return it is taken
                ViewBag.CreateUserError = "Username already exists.";
                return View(user);
                }
            //Create new admin user object and fill it our using bcrypt
            AdminUser newUser = new AdminUser
                {
                UserName = user.UserName,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password)
                };
            //Add the new user to the database and save it.
            _context.AdminUsers.Add(newUser);
            _context.SaveChanges();


            return RedirectToAction("Index", "Product");
            }

        /*[HttpPost]
        public IActionResult AdminLogin(AdminUser user)
            {
            if (user.Password.Equals("SecurePassword123"))
                {
                HttpContext.Session.SetString("Authenticated", "True");
                HttpContext.Session.SetString("uiserid", "3455");
                }
            return RedirectToAction("Index", "Product");

            }*/
        }
    }
