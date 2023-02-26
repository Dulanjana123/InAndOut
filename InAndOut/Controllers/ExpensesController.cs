using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InAndOut.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public ExpensesController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Expenses> objList = _applicationDBContext.Expenses;

            foreach(var obj in objList)
            {
                obj.ExpenseType = _applicationDBContext.ExpenseType.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }

            return View(objList);
        }

        //Get create
        public IActionResult Create()
        {
            ExpensesVM expensesVM = new ExpensesVM()
            {
                Expenses = new Expenses(),
                TypeDropDown = _applicationDBContext.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(expensesVM);
        }

        //POST create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpensesVM obj)
        {
            //server side validation
            if (ModelState.IsValid)
            {
                _applicationDBContext.Expenses.Add(obj.Expenses);
                _applicationDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //server side validation
            var obj = _applicationDBContext.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _applicationDBContext.Expenses.Find(id);
            if(obj == null)
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
            ExpensesVM expensesVM = new ExpensesVM()
            {
                Expenses = new Expenses(),
                TypeDropDown = _applicationDBContext.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            //server side validation
            expensesVM.Expenses = _applicationDBContext.Expenses.Find(id);
            if (expensesVM == null)
            {
                return NotFound();
            }
            return View(expensesVM);
        }

        //POST update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpensesVM obj)
        {
            if (ModelState.IsValid)
            {
                _applicationDBContext.Update(obj.Expenses);
                _applicationDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
