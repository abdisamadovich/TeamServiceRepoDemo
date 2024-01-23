using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _config;

    public UserService(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
    {
        _repository = userRepository;
        _tokenSerivce = tokenService;
        _config = configuration.GetSection("Jwt");
    }

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

    public UserViewModel GetById(string email)
    {
        var result = _repository.GetAll()
                .Select(x => new UserViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Password = x.Password,
                    Email = x.Email,
                })
                .FirstOrDefault(x => x.Email == email);

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
            FirstName = x.FirstName,
            LastName = x.LastName,
            Password = x.Password,
            Email = x.Email
        }).ToList();
    }

    public bool Register(UserViewModel userViewModel)
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

        var passwordHash = PasswordHasher.Hasher(userViewModel.Password);
        User user = new User
        {
            FirstName = userViewModel.FirstName,
            LastName = userViewModel.LastName,
            Email = userViewModel.Email,
            Password = passwordHash.Hash,
            Salt = passwordHash.Salt,
        };

        _repository.Insert(user);
        int result = _repository.SaveChanges();

        return result > 0;
    }

    public void Update(UserViewModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var result = _repository.GetAll().Where(x => x.Email == user.Email).FirstOrDefault();

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

    public (string access_token, string refresh_token, string token_type, long expires) Login(LoginViewModel loginViewModel)
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
            long expires_time = long.Parse(_config["Lifetime"]) * 3600;
            return (access_token: token, refresh_token: null, token_type: "Bearer", expires: expires_time);
        }
        else
        {
            throw new ParameterInvalidException("Invalid password");
        }
    }
}

