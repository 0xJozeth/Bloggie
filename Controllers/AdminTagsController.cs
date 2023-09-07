using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        async public Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping AddtagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]

        async public Task<IActionResult> List() 
        {

            // instantiate variable - Use dbContext to read the tags
            var tags = await bloggieDbContext.Tags.ToListAsync();

                // provide the variable
                return View(tags);
        }


        [HttpGet]
        async public Task<IActionResult> Edit(Guid id) 
        { 
            //var tag = bloggieDbContext.Tags.Find(id);

            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

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

        async public Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null) 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes
                await bloggieDbContext.SaveChangesAsync();

                // Show success notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }


            // Show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]

        async public Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);

            if (tag != null) 
            {
                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();

                // Show a success notification 
                return RedirectToAction("List");
            }

            // Show an error notification 
            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }

    }
}
