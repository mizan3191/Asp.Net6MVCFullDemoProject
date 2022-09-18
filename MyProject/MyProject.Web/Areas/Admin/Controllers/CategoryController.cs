using Microsoft.AspNetCore.Mvc;
using MyProject.DataAccessLayer.Infrastructure.IRepositories;
using MyProject.Models;
using MyProject.Models.ViewModels;

namespace MyProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWorks _unitOfWork;
        public CategoryController(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            CategoryVM categoryVM = new();
            categoryVM.Categories = _unitOfWork.CategoryRepository.GetAll();
            return View(categoryVM);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    Category model = new();
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.CategoryRepository.Add(category);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category is successfully Created";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View();
        //}

        [HttpGet]
        public ActionResult CreateUpdate(int? id)
        {
            CategoryVM categoryVM = new CategoryVM();
            if (id == 0 || id == null)
            {
                return View(categoryVM);
            }
            else
            {
                categoryVM.Category = _unitOfWork.CategoryRepository.GetById(x => x.Id == id);
                if (categoryVM.Category == null)
                {
                    return NotFound();
                }
                return View(categoryVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM categoryVM)
        {
            if (categoryVM.Category.Id == 0)
            {
                _unitOfWork.CategoryRepository.Add(categoryVM.Category);
                TempData["success"] = "Category is successfully Created";
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(categoryVM.Category);
                TempData["success"] = "Category is successfully Updated";
            }

            try
            {
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryRepository.GetById(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int? id)
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.GetById(x => x.Id == id);

                if (category != null)
                {
                    _unitOfWork.CategoryRepository.Delete(category);
                    _unitOfWork.Save();
                    TempData["success"] = "Category is successfully Deleted";
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
