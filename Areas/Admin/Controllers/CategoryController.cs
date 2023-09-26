using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            // choose what repository to use in unit of work
            List<Category> objCategoryList = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(objCategoryList);
        }

        // get
        public IActionResult CreateCategory()
        {
            return View();
        }

        //set
        [HttpPost]
        public IActionResult CreateCategory(Category obj)
        {
            // add custom validation
            // if it is integer
            if (int.TryParse(obj.Name, out _))
            {
                ModelState.AddModelError("Name", "Category Name cannot be numeric!");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(obj); // keep track changes
                _unitOfWork.Save(); // commit the changes
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index", "Category");
            }

            return View();


        }

        public IActionResult EditCategory(int? id)
        {
            // input id is not valid
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // find in the databse
            Category? found = _unitOfWork.CategoryRepository.GetFirstorDefault(u => u.Id == id);

            // if not found in the database
            if (found == null)
            {
                return NotFound();
            }

            return View(found);
        }

        //set
        [HttpPost]
        public IActionResult EditCategory(Category obj)
        {
            // add custom validation
            // if it is integer
            if (int.TryParse(obj.Name, out _))
            {
                ModelState.AddModelError("Name", "Category Name cannot be numeric!");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(obj); // keep track changes
                _unitOfWork.Save(); // commit the changes
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index", "Category");
            }

            return View();


        }

        // delete category
        public IActionResult DeleteCategory(int? id)
        {
            // input id is not valid
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // find in the databse
            Category? found = _unitOfWork.CategoryRepository.GetFirstorDefault(u => u.Id == id);

            // if not found in the database
            if (found == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepository.Remove(found);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index", "Category");
        }

    }
}
