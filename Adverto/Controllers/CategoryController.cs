using Adverto.Domain;
using Adverto.Dto.Category;
using Adverto.Repositories.CategoryRepository;
using Adverto.Routes;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet(RoutesAPI.CategoryRoutes.getCategory)]
        public async Task<IActionResult> GetCategory([FromRoute]Guid categoryId)
        {
            var category = await _repo.getCategoryAsync(categoryId);

            if (category != null)
                return Ok(_mapper.Map<CategoryResponse>(category));


            return NotFound();
        }
        [HttpGet(RoutesAPI.CategoryRoutes.getCategories)]
        public async Task<IActionResult> GetCategories()
        {

            var categoriesToMap = await _repo.getCategoriesAsync();
            var categories = _mapper.Map<List<CategoryResponse>>(categoriesToMap);

            return Ok(categories);
        }
        [HttpDelete(RoutesAPI.CategoryRoutes.removeCategory)]
        public async Task<IActionResult> RemoveCategory([FromRoute] Guid categoryId)
        {
            var result = await _repo.removeCategoryAsync(categoryId);

            if(result)
            {
                return Ok();
            }


            return NotFound();
        }
        [HttpPost(RoutesAPI.CategoryRoutes.addCategory)]
        public async Task<IActionResult> AddCategory([FromBody]CategoryRequest request)
        {


            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,

            };

           var result = await _repo.addCategoryAsync(category);

            if(result)
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var location = baseUrl + RoutesAPI.CategoryRoutes.getCategory.Replace("{categoryId}", category.Id.ToString());
                return Created(location,_mapper.Map<CategoryResponse>(category));
            }
            return NotFound();

        }
        [HttpPut(RoutesAPI.CategoryRoutes.updateCategory)]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryRequest request,[FromRoute]Guid categoryId)
        {
            var category = await _repo.getCategoryAsync(categoryId);

            category.Name = request.Name;

           var result =  await _repo.updateCategoryAsync(category);

            if (result)
                return Ok();


            return NotFound();
        }

    }


}
