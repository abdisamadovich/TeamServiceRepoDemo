using NTierApplication.DataAccess.Models;

namespace NTierApplication.Repository;

public interface IUserRepository
{
    void Insert(User user);
    void Update(User user);
    void Delete(User user);
    IQueryable<User> GetAll();
    int SaveChanges();
}
