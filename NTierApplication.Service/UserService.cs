using NTierApplication.DataAccess.Models;
using NTierApplication.Errors;
using NTierApplication.Repository;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public class UserService : IUserService
{
    private readonly IUserRepository UserRepository;

    public UserService(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }
    public void CreateNew(UserViewModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        if (string.IsNullOrWhiteSpace(user.FirstName))
        {
            throw new ParameterInvalidException("FirstName cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            throw new ParameterInvalidException("LastName cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(user.Password))
        {
            throw new ParameterInvalidException("Password cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ParameterInvalidException("Password cannot be empty");
        }

        var entity = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Email = user.Email,
        };
        UserRepository.Insert(entity);
        UserRepository.SaveChanges();
        user.Id = entity.Id;
    }

    public void Delete(int id)
    {
        var result = UserRepository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        if (result == null)
        {
            throw new ArgumentNullException(nameof(Item));
        }
        UserRepository.Delete(result);
        UserRepository.SaveChanges();
    }

    public UserViewModel GetById(int id)
    {
        var result = UserRepository.GetAll()
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Password = x.Password,
                    Email = x.Email,
                })
                .FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            throw new EntryNotFoundException("No such user");
        }
        return result;
        //.Where(x => x.Id == id)
        //.FirstOrDefault();
    }

    public ICollection<UserViewModel> GetUsers()
    {
        return UserRepository.GetAll().Select(x => new UserViewModel
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Password = x.Password,
            Email = x.Email
        }).ToList();
    }

    public void Update(UserViewModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var result = UserRepository.GetAll().Where(x => x.Id == user.Id).FirstOrDefault();

        if (result == null)
        {
            throw new EntryNotFoundException("No such user");
        }

        result.FirstName = user.FirstName;
        result.LastName = user.LastName;
        result.Password = user.Password;
        result.Email = user.Email;

        UserRepository.Update(result);
        UserRepository.SaveChanges();
    }
}

