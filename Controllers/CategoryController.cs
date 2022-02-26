using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication17.Data;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }



        public IActionResult Index()
        {
            IEnumerable<Category> result = _db.Categories.ToList();
            return View(result);
        }


        public IActionResult Create()
        {
            
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError","The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null ||id==0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var catgoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var catgoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb ==null)
            {
                return NotFound();
            }


            return View(categoryFromDb);
        }


    }
}
