﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository) 
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet] //explicitly declare the get method as http
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping AddtagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);
            
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]

        public async Task<IActionResult> List() 
        {

            // instantiate variable - Use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

                // provide the variable
                return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        { 
            var tag = await tagRepository.GetAsync(id);

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

        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null) 
            {
                // Show success notification
            }
            else
            {
                // Show error notification

            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null) 
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification 
            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }

    }
}
