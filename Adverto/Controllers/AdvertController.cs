using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Domain;
using Adverto.Dto.AdvertDto;
using Adverto.Repositories;
using Adverto.Routes;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adverto.Controllers
{
   
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdvertController : ControllerBase
    {

        private readonly IAdvertRepo _advertRepo;
        private readonly IMapper _mapper;
        public AdvertController(IAdvertRepo advertRepo,IMapper mapper)
        {
            _advertRepo = advertRepo;
            _mapper = mapper;
        }
        [HttpGet(RoutesAPI.AdvertRoutes.GetAdverts)]
        public async Task<IActionResult> getAdverts()
        {
           var adverts =  await _advertRepo.getAdvertsAsync();

            return Ok(_mapper.Map<List<AdvertResponse>>(adverts));
        }
        [HttpGet(RoutesAPI.AdvertRoutes.GetAdvert)]
        public async Task<IActionResult> GetAdvert([FromRoute]Guid advertId)
        {
            var advert = await _advertRepo.getAdvertAsync(advertId);

            if (advert == null)
                return NoContent();

            return Ok(_mapper.Map<AdvertResponse>(advert));
        }
        [HttpDelete(RoutesAPI.AdvertRoutes.RemoveAdvert)]
        public async Task<IActionResult> RemoveAdvert([FromRoute]Guid advertId)
        {
            var result = await _advertRepo.deleteAdvertAsync(advertId);

            if (result == false)
                return NotFound();


            return Ok();
        }
        [HttpPost(RoutesAPI.AdvertRoutes.addAdvert)]
        public async Task<IActionResult> AddAdvert([FromBody] AdvertRequest request)
        {

            var advert = _mapper.Map<Advert>(request);
            advert.Id = Guid.NewGuid();
          

           await _advertRepo.addAdvertAsync(advert);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = baseUrl + RoutesAPI.AdvertRoutes.GetAdvert.Replace("{advertId}", advert.Id.ToString());

           
                return Created(location,_mapper.Map<AdvertResponse>(advert));      
        }
        [HttpPut(RoutesAPI.AdvertRoutes.updateAdvert)]
        public async Task<IActionResult>  UpdateAdvert([FromRoute]Guid advertId,[FromBody]AdvertRequest request)
        {
            var advertToUpdate = await _advertRepo.getAdvertAsync(advertId);

            
            advertToUpdate.Description = request.Description;
            advertToUpdate.Location = request.Location;
            advertToUpdate.Name = request.Name;
            advertToUpdate.Prize =request.Prize;
           

            var status = await _advertRepo.updateAdvertAsync(advertToUpdate);


            if (status)
                return Ok(_mapper.Map<AdvertResponse>(advertToUpdate));



            return NotFound();


        }


    }
}