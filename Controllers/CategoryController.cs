using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedliu.DataAccess;
using WebApplication17.Models;
using Tedliu.DataAccess.Repository;

namespace WebApplication17.Controllers
{
    public class CategoryController : Controller
    {
        private readonly  ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db=db;
        }



        public IActionResult Index()
        {
            IEnumerable<Category> result = _db.GetAll();
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
                _db.Add(obj);
                _db.Save();
                TempData["success"]="Category created successs fully";
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
            //var categoryFromDb = _db.Categories.Find(id);
            var catgoryFromDbFirst = _db.GetFirstOrDefault(u => u.Name == "id");
            //var catgoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (catgoryFromDbFirst == null)
            {
                return NotFound();
            }


            return View(catgoryFromDbFirst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated successs fully";
                return RedirectToAction("Index");

            }

            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var catgoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //var catgoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (catgoryFromDbFirst == null)
            {
                return NotFound();
            }


            return View(catgoryFromDbFirst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(u=>u.Id==id);

            if (id == null || id == 0)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category delete successs fully";
            return RedirectToAction("Index");
        }

    }
}
