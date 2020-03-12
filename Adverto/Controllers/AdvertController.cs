using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Domain;
using Adverto.Repositories;
using Adverto.Routes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adverto.Controllers
{
   
    [ApiController]
    public class AdvertController : ControllerBase
    {

        private readonly IAdvertRepo _advertRepo;
        public AdvertController(IAdvertRepo advertRepo)
        {
            _advertRepo = advertRepo;
        }
        [HttpGet(RoutesAPI.AdvertRoutes.GetAdverts)]
        public async Task<IActionResult> getAdverts()
        {
           var adverts = await _advertRepo.getAdvertsAsync();

            return Ok(adverts);
        }
        [HttpGet(RoutesAPI.AdvertRoutes.GetAdvert)]
        public async Task<IActionResult> getAdvert([FromRoute]Guid advertId)
        {
            var advert = await _advertRepo.getAdvertAsync(advertId);

            if (advert == null)
                return NoContent();

            return Ok(advert);
        }
        [HttpDelete(RoutesAPI.AdvertRoutes.RemoveAdvert)]
        public async Task<IActionResult> removeAdvert([FromRoute]Guid advertId)
        {
            var result = await _advertRepo.deleteAdvertAsync(advertId);

            if (result == false)
                return NotFound();


            return Ok();
        }
        [HttpPost(RoutesAPI.AdvertRoutes.addAdvert)]
        public async Task<IActionResult> addAdvert([FromBody] Advert advert)
        {

            advert.Id = Guid.NewGuid();

          var result =  await _advertRepo.addAdvertAsync(advert);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = baseUrl + RoutesAPI.AdvertRoutes.GetAdvert.Replace("{advertId}", advert.Id.ToString());

           
                return Created(location,advert);      
        }
        [HttpPut(RoutesAPI.AdvertRoutes.updateAdvert)]
        public async Task<IActionResult>  updateAdvert([FromRoute]Guid advertId,[FromBody]Advert advert)
        {
            var advertToUpdate = await _advertRepo.getAdvertAsync(advertId);

            advertToUpdate.Category = advert.Category;
            advertToUpdate.Description = advert.Description;
            advertToUpdate.Location = advert.Location;
            advertToUpdate.Name = advert.Name;
            advertToUpdate.Prize = advert.Prize;
           

            var status = await _advertRepo.updateAdvertAsync(advertToUpdate);


            if (status)
                return Ok(advertToUpdate);



            return NotFound();


        }


    }
}