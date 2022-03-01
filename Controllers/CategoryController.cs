using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedliu.DataAccess;
using WebApplication17.Models;
using Tedliu.DataAccess.Repository;
using Tedliu.DataAccess.Repository.IRepositoryS;

namespace WebApplication17.Controllers
{
    public class CategoryController : Controller
    {
        private readonly  IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
           _unitOfWork=unitOfWork;
        }



        public IActionResult Index()
        {
            IEnumerable<Category> result = _unitOfWork.Category.GetAll();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
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
            var catgoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Name == "id");
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            var catgoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
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
            var obj = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);

            if (id == null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category delete successs fully";
            return RedirectToAction("Index");
        }

    }
}
