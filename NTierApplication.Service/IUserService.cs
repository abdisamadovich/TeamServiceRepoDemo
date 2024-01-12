using NTierApplication.DataAccess.Models;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public interface IUserService
{
    public (bool Result, string Token) Register(UserViewModel userViewModel);
    void Update(UserViewModel user);
    void Delete(int id);
    ICollection<UserViewModel> GetUsers();
    UserViewModel GetById(int id);
    public (bool Result, string Token) Login(LoginViewModel loginViewModel);
}
