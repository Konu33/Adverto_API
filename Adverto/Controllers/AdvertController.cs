using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Domain;
using Adverto.Dto.AdvertDto;
using Adverto.Repositories;
using Adverto.Routes;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adverto.Controllers
{
   
    [ApiController]

    public class AdvertController : ControllerBase
    {

        private readonly IAdvertRepo _advertRepo;
        private readonly IMapper _mapper;
        public readonly IWebHostEnvironment _enviroment;
        public AdvertController(IAdvertRepo advertRepo,IMapper mapper,IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(RoutesAPI.AdvertRoutes.RemoveAdvert)]
        public async Task<IActionResult> RemoveAdvert([FromRoute]Guid advertId)
        {
            var result = await _advertRepo.deleteAdvertAsync(advertId);

            if (result == false)
                return NotFound();


            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(RoutesAPI.AdvertRoutes.addAdvert)]
        public async Task<IActionResult> AddAdvert([FromForm]AdvertRequest request)
        {

            var advert = _mapper.Map<Advert>(request);
            advert.Id = Guid.NewGuid();


            
            try
            {
                if(request.file == null)
                {
                    advert.PhotoUrl = "";
                }
                else
                {
                    if (request.file.Length > 0)
                    {
                        if (!Directory.Exists(_enviroment.WebRootPath + "\\Upload\\"))
                        {
                            Directory.CreateDirectory(_enviroment.WebRootPath + "\\Upload\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_enviroment.WebRootPath + "\\Upload\\" + request.file.FileName))
                        {
                            request.file.CopyTo(fileStream);
                            fileStream.Flush();
                            advert.PhotoUrl = fileStream.Name;

                            using (var ms = new MemoryStream())
                            {
                                request.file.CopyTo(ms);
                                advert.ImageByte = ms.ToArray();
                            }

                        }
                    }
                    else
                    {
                        advert.PhotoUrl = "";
                    }
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

         
            await _advertRepo.addAdvertAsync(advert);


            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = baseUrl + RoutesAPI.AdvertRoutes.GetAdvert.Replace("{advertId}", advert.Id.ToString());



            return Created(location, _mapper.Map<AdvertResponse>(advert));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(RoutesAPI.AdvertRoutes.updateAdvert)]
        public async Task<IActionResult>  UpdateAdvert([FromRoute]Guid advertId,[FromForm]AdvertRequest request)
        {
            var advertToUpdate = await _advertRepo.getAdvertAsync(advertId);

            
            advertToUpdate.Description = request.Description;
            advertToUpdate.Location = request.Location;
            advertToUpdate.Name = request.Name;
            advertToUpdate.Prize =request.Prize;
            advertToUpdate.CategoryId = request.CategoryId;
            try
            {
                if (request.file == null)
                {
                    advertToUpdate.PhotoUrl = "";
                }
                else
                {
                    if (request.file.Length > 0)
                    {
                        if (!Directory.Exists(_enviroment.WebRootPath + "\\Upload\\"))
                        {
                            Directory.CreateDirectory(_enviroment.WebRootPath + "\\Upload\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_enviroment.WebRootPath + "\\Upload\\" + request.file.FileName))
                        {
                            request.file.CopyTo(fileStream);
                            fileStream.Flush();
                            advertToUpdate.PhotoUrl = fileStream.Name;

                            using (var ms = new MemoryStream())
                            {
                                request.file.CopyTo(ms);
                                advertToUpdate.ImageByte = ms.ToArray();
                            }

                        }
                    }
                    else
                    {
                        advertToUpdate.PhotoUrl = "";
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            var status = await _advertRepo.updateAdvertAsync(advertToUpdate);


            if (status)
                return Ok(_mapper.Map<AdvertResponse>(advertToUpdate));



            return NotFound();


        }


    }
}