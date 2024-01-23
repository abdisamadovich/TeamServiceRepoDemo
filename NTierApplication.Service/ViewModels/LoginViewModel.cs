namespace NTierApplication.Service.ViewModels;

public class LoginViewModel
{
    public string GrantType { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long ClientId { get; set; }
}
