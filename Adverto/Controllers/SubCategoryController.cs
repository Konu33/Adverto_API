using Adverto.Domain;
using Adverto.Dto.SubCategoriesDto;
using Adverto.Repositories.SubCategoryRepo;
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
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCatergoryRepo _repo;
        private readonly IMapper _mapper;
        public SubCategoryController(ISubCatergoryRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet(RoutesAPI.SubCategory.getSubCategories)]
        public async Task<IActionResult> GetCategories()
        {
            var subCat = await _repo.getSubCategoriesAsync();
            var subCategories = _mapper.Map<List<SubCategoryResponse>>(subCat);

            return Ok(subCategories);
        }
        [HttpGet(RoutesAPI.SubCategory.getSubCategory)]
        public async Task<IActionResult> getCategory([FromRoute]Guid subcategoryId) 
        {
            var subCat = await _repo.getSubCategoryAsync(subcategoryId);
            var subCategory =_mapper.Map<SubCategoryResponse>(subCat);

            if(subCategory != null)
            {
                return Ok(subCategory);
            }

            return BadRequest();
        }
        [HttpPost(RoutesAPI.SubCategory.createSubCategory)]
        public async Task<IActionResult> AddSubCategory([FromBody]SubCategoryRequest request)
        {

            var subCategory = _mapper.Map<SubCategory>(request);
            subCategory.Id = Guid.NewGuid();

            var result = await _repo.addSubCategoryAsync(subCategory);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = baseUrl + RoutesAPI.SubCategory.getSubCategory.Replace("{subcategoryId}", subCategory.Id.ToString());


            if (result)
            {
                return Created(location,_mapper.Map<SubCategoryResponse>(subCategory));
            }
            return BadRequest();

        }
        [HttpDelete(RoutesAPI.SubCategory.removeSubCategory)]
        public async Task<IActionResult> RemoveSubCategory([FromRoute]Guid subcategoryId)
        {
            var result = await _repo.removeSubCategoryAsync(subcategoryId);

            if(result)
            {
                return Ok();
            }

            return NotFound();
        }
        [HttpPut(RoutesAPI.SubCategory.updateSubCategory)]
        public async Task<IActionResult> UpdateSubCategory([FromBody]SubCategoryRequest request,[FromRoute]Guid subcategoryId)
        {

            var subCategory = await _repo.getSubCategoryAsync(subcategoryId);
            subCategory.Name = request.Name;
           


            var result = await _repo.updateSubCategoryAsync(subCategory);

            if(result)
            {
                return Ok();

            }

            return BadRequest();
        }
    }
}
