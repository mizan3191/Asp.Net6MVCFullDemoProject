using Microsoft.AspNetCore.Mvc;
using MyProject.DataAccess.UnitOfWorks;
using MyProject.Domain;

namespace MyProject.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private IUnitOfWorks _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWorks unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category");
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}