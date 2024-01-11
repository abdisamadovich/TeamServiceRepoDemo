using Microsoft.EntityFrameworkCore;
using NTierApplication.DataAccess;
using NTierApplication.DataAccess.Models;

namespace NTierApplication.Repository;

public class UserRepository : IUserRepository
{
    private MainContext DbContext;

    public UserRepository(MainContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Delete(User user)
    {
        if (DbContext.Entry(user).State != EntityState.Deleted)
        {
            DbContext.Users.Remove(user);
        }
    }

    public IQueryable<User> GetAll()
    {
        return DbContext.Users;
    }

    public void Insert(User user)
    {
        DbContext.Users.Add(user);
    }

    public int SaveChanges()
    {
        return DbContext.SaveChanges();
    }

    public void Update(User user)
    {
        if (DbContext.Entry(user).State != EntityState.Modified)
        {
            DbContext.Entry(user).State = EntityState.Modified;
        }
    }
}
