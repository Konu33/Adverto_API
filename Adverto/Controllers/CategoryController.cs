using Adverto.Domain;
using Adverto.Repositories.CategoryRepository;
using Adverto.Routes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }
        [HttpGet(RoutesAPI.CategoryRoutes.getCategory)]
        public async Task<IActionResult> getCategory([FromRoute]Guid categoryId)
        {
            var category = await _repo.getCategoryAsync(categoryId);

            if (category != null)
                return Ok(category);


            return NotFound();
        }
        [HttpGet(RoutesAPI.CategoryRoutes.getCategories)]
        public async Task<IActionResult> getCategories()
        {
            return Ok(await _repo.getCategoriesAsync());
        }
        [HttpDelete(RoutesAPI.CategoryRoutes.removeCategory)]
        public async Task<IActionResult> removeCategory([FromRoute] Guid categoryId)
        {
            var result = await _repo.removeCategoryAsync(categoryId);

            if(result)
            {
                return Ok();
            }


            return NotFound();
        }
        [HttpPost(RoutesAPI.CategoryRoutes.addCategory)]
        public async Task<IActionResult> AddCategory([FromBody]Category category)
        {

            category.Id = Guid.NewGuid();
           var result = await _repo.addCategoryAsync(category);

            if(result)
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var location = baseUrl + RoutesAPI.CategoryRoutes.getCategory.Replace("{categoryId}", category.Id.ToString());
                return Created(location,category);
            }
            return NotFound();

        }
        [HttpPut(RoutesAPI.CategoryRoutes.updateCategory)]
        public async Task<IActionResult> UpdateCategory([FromBody]Category category,[FromRoute]Guid categoryId)
        {
            category.Id = categoryId;

           var result =  await _repo.updateCategoryAsync(category);

            if (result)
                return Ok(result);


            return NotFound();
        }

    }


}
