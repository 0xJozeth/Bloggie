using Bloggie.Web.Models.ViewModels;
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

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {

            var name = addTagRequest.Name;
            var displayName = addTagRequest.DisplayName;
            
            return View("Add");
        }
    }
}
