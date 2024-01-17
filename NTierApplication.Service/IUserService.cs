using NTierApplication.DataAccess.Models;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public interface IUserService
{
    public bool Register(UserViewModel userViewModel);
    public (string access_token, string refresh_token, string token_type, long expires) Login(LoginViewModel loginViewModel);
    void Update(UserViewModel user);
    void Delete(int id);
    ICollection<UserViewModel> GetUsers();
    UserViewModel GetById(string email);
    
}
