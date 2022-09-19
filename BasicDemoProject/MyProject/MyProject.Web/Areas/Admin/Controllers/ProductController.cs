using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.DataAccess.UnitOfWorks;
using MyProject.Domain.ViewModels;

namespace MyProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IUnitOfWorks _unitOfWork;

        public ProductController(IUnitOfWorks unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        #region APICALL
        public IActionResult ALlProduct()
        {
            var product = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category");
            //return View(product);
            return Json(new { data = product });
        }

        #endregion

        public ActionResult Index()
        {
            //ProductVM ProductVM = new();
            //ProductVM.Products = _unitOfWork.ProductRepository.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult CreateUpdate(int? id)
        {
            ProductVM ProductVM = new ProductVM()
            {
                Product = new(),
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(x =>
                new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            if (id == 0 || id == null)
            {
                return View(ProductVM);
            }
            else
            {
                ProductVM.Product = _unitOfWork.ProductRepository.GetById(x => x.Id == id);
                if (ProductVM.Product == null)
                {
                    return NotFound();
                }
                return View(ProductVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM ProductVM, IFormFile? file)
        {
            string fileName = String.Empty;
            if (file != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");
                fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                if (ProductVM.Product.ImageUrl != null)
                {
                    var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                ProductVM.Product.ImageUrl = @"\ProductImage\" + fileName;
            }

            if (ProductVM.Product.Id == 0)
            {
                _unitOfWork.ProductRepository.Add(ProductVM.Product);
                TempData["success"] = "Product is successfully Created";
            }
            else
            {
                _unitOfWork.ProductRepository.Update(ProductVM.Product);
                TempData["success"] = "Product is successfully Updated";
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

        #region DeleteAPI

        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            var product = _unitOfWork.ProductRepository.GetById(x => x.Id == id);

            if (product != null)
            {
                var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }

                _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Product is successfully Deleted" });
            }
            else
            {
                return Json(new { success = false, message = "Product is Emply" });
            }
        }
        #endregion
    }
}
