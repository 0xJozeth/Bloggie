using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext) 
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet] //explicitly declare the get method as http
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            // Mapping AddtagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]

        public IActionResult List() 
        {

            // instantiate variable - Use dbContext to read the tags
            var tags = bloggieDbContext.Tags.ToList();

                // provide the variable
                return View(tags);
        }


        [HttpGet]
        public IActionResult Edit(Guid id) 
        { 
            //var tag = bloggieDbContext.Tags.Find(id);

            var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);   
            }
            return View(null); 
        }

        [HttpPost]

        public IActionResult Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = bloggieDbContext.Tags.Find(tag.Id);

            if (existingTag != null) 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes
                bloggieDbContext.SaveChanges();

                // Show success notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }


            // Show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
