using Adverto.Domain;
using Adverto.Repositories.SubCategoryRepo;
using Adverto.Routes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Controllers
{
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCatergoryRepo _repo;
        public SubCategoryController(ISubCatergoryRepo repo)
        {
            _repo = repo;
        }
        [HttpGet(RoutesAPI.SubCategory.getSubCategories)]
        public async Task<IActionResult> getCategories()
        {
            return Ok(await _repo.getSubCategoriesAsync());
        }
        [HttpGet(RoutesAPI.SubCategory.getSubCategory)]
        public async Task<IActionResult> getCategory([FromRoute]Guid subcategoryId)
        {
            var subCategory = await _repo.getSubCategoryAsync(subcategoryId);

            if(subCategory != null)
            {
                return Ok(subcategoryId);
            }

            return BadRequest();
        }
        [HttpPost(RoutesAPI.SubCategory.createSubCategory)]
        public async Task<IActionResult> addSubCategory([FromBody]SubCategory subCategory)
        {
            var result = await _repo.addSubCategoryAsync(subCategory);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = baseUrl + RoutesAPI.SubCategory.getSubCategory.Replace("{subcategoryId}", subCategory.Id.ToString());


            if (result)
            {
                return Created(location, subCategory);
            }
            return BadRequest();

        }
        [HttpDelete(RoutesAPI.SubCategory.removeSubCategory)]
        public async Task<IActionResult> removeSubCategory([FromRoute]Guid subcategoryId)
        {
            var result = await _repo.removeSubCategoryAsync(subcategoryId);

            if(result)
            {
                return Ok();
            }

            return NotFound();
        }
        [HttpPut(RoutesAPI.SubCategory.updateSubCategory)]
        public async Task<IActionResult> updateSubCategory([FromBody]SubCategory subCategory,[FromRoute]Guid subcategoryId)
        {
            subCategory.Id = subcategoryId;


            var result = await _repo.updateSubCategoryAsync(subCategory);

            if(result)
            {
                return Ok();

            }

            return BadRequest();
        }
    }
}
