using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _db.Categories.ToList();
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

			if(ModelState.IsValid)
			{
				_db.Categories.Add(obj); // keep track changes
				_db.SaveChanges(); // commit the changes
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
			Category? found = _db.Categories.Find(id);

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

			if(ModelState.IsValid)
			{
				_db.Categories.Update(obj); // keep track changes
				_db.SaveChanges(); // commit the changes
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
			Category? found = _db.Categories.Find(id);

			// if not found in the database
			if (found == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(found);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully!";
			return RedirectToAction("Index", "Category");
		}

	}
}
