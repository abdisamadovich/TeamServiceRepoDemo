using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service;
using NTierApplication.Service.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public ICollection<UserViewModel> GetAll()
        {
            return UserService.GetUsers();
        }

        /*[HttpPost(Name = "CreateNew1")]
        public UserViewModel CreateNew(UserViewModel userViewModel)
        {
            userViewModel.Password = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password);
            UserService.CreateNew(userViewModel);
            return userViewModel;
        }*/

        [HttpGet]
        [Route("{id}")]
        public UserViewModel GetById(string id)
        {
            return UserService.GetById(id);
        }

        /*[HttpPut]
        public UserViewModel Update(UserViewModel userViewModel)
        {
            UserService.Update(userViewModel);
            return userViewModel;
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            UserService.Delete(id);
        }*/
    }
}
