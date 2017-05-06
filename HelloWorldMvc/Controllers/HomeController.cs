using System.Web.Mvc;
using HelloWorldMvc.Models;

namespace HelloWorldMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.WelcomeMessage = "Welcome";
            var customer = new CustomerViewModel {Name = "John"};
            return View(customer);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}