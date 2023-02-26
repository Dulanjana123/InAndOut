using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDBContext _applicationDbContext;

        public ItemController(ApplicationDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Item> objList = _applicationDbContext.Items;
            return View(objList);
        }

        //Get create
        public IActionResult Create()
        {
            
            return View();
        }

        //POST create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item obj)
        {
            _applicationDbContext.Items.Add(obj);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
