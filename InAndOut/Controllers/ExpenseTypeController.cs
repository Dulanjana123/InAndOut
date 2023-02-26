using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public ExpenseTypeController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public IActionResult Index()
        {
            IEnumerable<ExpenseType> objList = _applicationDBContext.ExpenseType;
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
        public IActionResult Create(ExpenseType obj)
        {
            //server side validation
            if (ModelState.IsValid)
            {
                _applicationDBContext.ExpenseType.Add(obj);
                _applicationDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET DELETE]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //server side validation
            var obg = _applicationDBContext.ExpenseType.Find(id);
            if (obg == null)
            {
                return NotFound();
            }
            return View(obg);
        }

        //POST delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _applicationDBContext.ExpenseType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            //server side validation

            _applicationDBContext.Remove(obj);
            _applicationDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET update
        public IActionResult update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //server side validation
            var obg = _applicationDBContext.ExpenseType.Find(id);
            if (obg == null)
            {
                return NotFound();
            }
            return View(obg);
        }

        //POST update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseType obj)
        {
            if (ModelState.IsValid)
            {
                _applicationDBContext.Update(obj);
                _applicationDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
