using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;
// Other usings
namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private DishContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(DishContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dishes> AllDishes = dbContext.dishes.ToList();
            ViewBag.Alldishes = AllDishes;
            return View("index");
            // Other code
        }
        [HttpGet("add")]
        public IActionResult CreateSplash()
        {
            return View("Create");
        }
        [HttpPost("Create")]
        public IActionResult AddDish(Dishes dish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                // Oh no!  We need to return a ViewResponse to preserve the ModelState, and the errors it now contains!
                return View("Create");
            }
        }
        [HttpGet("dish/{dishid}")]
        public IActionResult ShowDish(int dishid)
        {
            List<Dishes> dish = dbContext.dishes.Where(id => id.Id == dishid).ToList();
            ViewBag.dish = dish;
            return View("show");
        }
        [HttpGet("delete/{dish_id}")]
        public IActionResult delete_dish(Dishes deleted_dish, int dish_id)
        {
            Dishes retrievedDish = dbContext.dishes.FirstOrDefault(dish => dish.Id == dish_id);
            dbContext.dishes.Remove(retrievedDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("edit/{dish_id}")]
        public IActionResult editDish(Dishes dish, int dish_id)
        {
            List<Dishes> retrievedDish = dbContext.dishes.Where(x => x.Id == dish_id).ToList();
            ViewBag.dish=retrievedDish;
            return View("edit");
        }
        [HttpPost("edit/{dish_id}")]
        public IActionResult updateDish(string name, string chef, int tastiness, int calories,string description, int dish_id)
        {
            List<Dishes> retrievedDish = dbContext.dishes.Where(x => x.Id == dish_id).ToList();
            retrievedDish[0].name=name;
            retrievedDish[0].chef=chef;
            retrievedDish[0].calories=calories;
            retrievedDish[0].tastiness=tastiness;
            retrievedDish[0].description=description;
            retrievedDish[0].updated_at= DateTime.Now;
            dbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}