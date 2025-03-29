using Microsoft.AspNetCore.Identity;

namespace WURS.Business.DTO;

public class RegisterModel : IdentityUser
{
    public required string RegisterSecret { get; init; }
    public required string Password { get; init; }
}
