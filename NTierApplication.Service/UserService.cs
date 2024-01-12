using Microsoft.AspNetCore.Identity;
using NTierApplication.DataAccess.Models;
using NTierApplication.Errors;
using NTierApplication.Repository;
using NTierApplication.Service.Helpers;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private ITokenService _tokenSerivce;

    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _repository = userRepository;
        _tokenSerivce = tokenService;
    }
    /*public void CreateNew(UserViewModel user)
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
            throw new ParameterInvalidException("Email cannot be empty");
        }

        var entity = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Email = user.Email,
        };
        _repository.Insert(entity);
        _repository.SaveChanges();
        user.Id = entity.Id;
    }*/

    public void Delete(int id)
    {
        var result = _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
        if (result == null)
        {
            throw new ArgumentNullException(nameof(Item));
        }
        _repository.Delete(result);
        _repository.SaveChanges();
    }

    public UserViewModel GetById(int id)
    {
        var result = _repository.GetAll()
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
        return _repository.GetAll().Select(x => new UserViewModel
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Password = x.Password,
            Email = x.Email
        }).ToList();
    }

    public List<User> Login(LoginViewModel loginViewModel)
    {
        var users = _repository.GetAll();

        return users.Where(x => x.Email == loginViewModel.Email).ToList();
    }

    public (bool Result, string Token) Register(UserViewModel userViewModel)
    {
        if (userViewModel == null)
        {
            throw new ArgumentNullException(nameof(userViewModel));
        }
        var userDatabase = _repository.GetAll().
            Where(x => x.Email == userViewModel.Email).
            FirstOrDefault();
        if (userDatabase != null)
        {
            throw new ParameterInvalidException(nameof(userViewModel));
        }

        User user = new User
        {
            FirstName = userViewModel.FirstName,
            LastName = userViewModel.LastName,
            Email = userViewModel.Email,
            Password = userViewModel.Password
        };

        _repository.Insert(user);
        int result = _repository.SaveChanges();

        if (result > 0)
        {
            string token = _tokenSerivce.GenerateToken(user);
            return (Result: true, Token: token);
        }
        else
        {
            return (Result: false, Token: "");
        }
    }

    public void Update(UserViewModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var result = _repository.GetAll().Where(x => x.Id == user.Id).FirstOrDefault();

        if (result == null)
        {
            throw new EntryNotFoundException("No such user");
        }

        result.FirstName = user.FirstName;
        result.LastName = user.LastName;
        result.Password = user.Password;
        result.Email = user.Email;

        _repository.Update(result);
        _repository.SaveChanges();
    }

    (bool Result, string Token) IUserService.Login(LoginViewModel loginViewModel)
    {
        if (loginViewModel == null)
        {
            throw new ArgumentNullException(nameof(loginViewModel));
        }
        var userDatabase = _repository.GetAll().
          Where(x => x.Email == loginViewModel.Email).
          FirstOrDefault();
        if (userDatabase == null)
        {
            throw new EntryNotFoundException(nameof(userDatabase));
        }

        var hasherResult = PasswordHasher.Verify(loginViewModel.Password, userDatabase.Password, userDatabase.Salt);
        if (hasherResult == true)
        {
            string token = _tokenSerivce.GenerateToken(userDatabase);
            return (Result: true, Token: token);
        }
        else
        {
            throw new ParameterInvalidException(nameof(loginViewModel));
        }

    }
}

