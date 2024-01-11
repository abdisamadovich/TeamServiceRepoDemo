using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public interface IUserService
{
    void CreateNew(UserViewModel user);
    void Update(UserViewModel user);
    void Delete(int id);
    ICollection<UserViewModel> GetUsers();
    UserViewModel GetById(int id);
}
