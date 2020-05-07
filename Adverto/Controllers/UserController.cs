using Adverto.Domain;
using Adverto.Dto.UserDto;
using Adverto.Repositories;
using Adverto.Routes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet(RoutesAPI.User.getUser)]
        public async Task<IActionResult> GetUser([FromRoute]Guid userId )
        {
            var user = await _repo.getUser(userId);

            if (user !=null)
                return Ok(_mapper.Map<UserResponse>(user));


            return NotFound();
        }
        [HttpPut(RoutesAPI.User.updateUser)]
        public async Task<IActionResult> UpdateUser([FromBody]UserRequest request,[FromRoute]Guid userId )
        {

            var user = await _repo.getUser(userId);
            user.Name = request.Name;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Surrname = request.Surrname;

            var updated = await _repo.updateUserAsync(user);

             
            if (updated)
                return Ok(_mapper.Map<UserResponse>(user));
            


            return NotFound();

        }
        [HttpGet(RoutesAPI.User.getUsers)]
        public async Task<IActionResult> GetUsers()
        {
            var userToMap = await _repo.getUsers();
            var users = _mapper.Map<List<UserResponse>>(userToMap);

            if (users != null)
                return Ok(users);

            return NotFound();

        }
        [HttpPost(RoutesAPI.User.createUser)]
        public async Task<IActionResult> CreateUser([FromBody]UserRequest request)
        {
            var userToCreate = _mapper.Map<User>(request);
            userToCreate.Id = Guid.NewGuid();
          

             

            await _repo.addUserAsync(userToCreate);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var location = baseUrl + RoutesAPI.User.getUser.Replace("{userId}", userToCreate.Id.ToString());


            return Created(location,_mapper.Map<UserResponse>(userToCreate));

        }
        [HttpDelete(RoutesAPI.User.removeUser)]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid userId)
        {
            var status = await _repo.removeUserAsync(userId);

            if (status)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    } 

}
