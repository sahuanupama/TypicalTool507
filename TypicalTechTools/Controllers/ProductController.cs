using TypicalTechTools.DataAccess;
using TypicalTechTools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalTools.Controllers
    {
    public class ProductController : Controller
        {
        private readonly TypicalToolDBContext _context;

        //dependecncy injection system of MVC via the program.cs class
        public ProductController(TypicalToolDBContext context)
            {
            _context = context;
            }

        // Show all products
        public IActionResult Index()
            {
            var products = _context.Products.AsEnumerable();
            return View(products);
            }


        public IActionResult Privacy()
            {
            return View();
            }


        public ActionResult ShowSearchResult(string searchPhrase)
            {
            string search = string.IsNullOrWhiteSpace(searchPhrase) ? "" : searchPhrase;
            //Stores the search term in the cookie so we can put it back in the form if needed.
            HttpContext.Session.SetString("LastUserSearch", search);
            var product = _context.Products.Where(a => a.ProductName.Contains(search)).AsEnumerable();
            // pass the Product collection to veiw.

            return View("Index", product);
            }

        // GET: ProductController1/Details/5
        public ActionResult Details(int id)
            {
            if (id == null || id <= 0)
                {
                return BadRequest();
                }
            //Request the users from database using the context class.
            var product = _context.Products.Find(id);
            if (product == null)
                {
                BadRequest();
                }

            return View(product);
            }

        // GET: ProductController1/Create
        public ActionResult Create()
            {
            return View();
            }

        // POST: ProductController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
            {
                {
                // ModelState.Remove("Products");
                if (ModelState.IsValid)
                    {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    }
                return RedirectToAction(nameof(Index));
                }
            }


        // GET: ProductController1/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
            {
            string authStatus = HttpContext.Session.GetString("IsAuthenticated");
            bool isLoggedIn = !String.IsNullOrWhiteSpace(authStatus) && authStatus.Equals("true");

            if (!isLoggedIn)
                {
                TempData["ProductError"] = "User not logged in.";
                return RedirectToAction("Index");
                }

            if (id == null || id <= 0)
                {
                return BadRequest();
                }
            var product = _context.Products.Find(id);
            if (product == null)
                {
                return BadRequest();
                }
            return View(product);
            }

        // POST: ProductController1/Edit/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
            {
            /*if (id == null || id <= 0)
                {
                return BadRequest();
                }*/
            if (_context.Products.Any(a => a.ProductCode == product.ProductCode) == false)
                {
                return BadRequest();
                }
            if (ModelState.IsValid)
                {
                _context.Products.Update(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
                }
                {
                return View(product);
                }
            }
        // GET: ProductController1/Delete/5
        public ActionResult Delete(int id)
            {
            if (id == 0 || id <= 0)
                {
                return BadRequest();
                }
            var product = _context.Products.Find(id);
                {
                if (id == null)
                    {
                    return BadRequest();
                    }
                }
            return View(product);
            }

        // POST: ProductController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
            {
            if (id == null || id <= 0)
                {
                return BadRequest();
                }
            //Check if a record exists that match the provided id.
            //It thid check is not done, potential errors might occour.
            if (_context.Products.Any(_a => _a.Id == id) == false)
                {
                return BadRequest();
                }
            //Flag the user to be removed from the database and process  the deletion.
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToActionPermanent(nameof(Index));
            }

        }
    }
