using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        
        [HttpGet] //explicitly declare the get method as http
        public IActionResult Add()
        {
            return View();
        }
    }
}
