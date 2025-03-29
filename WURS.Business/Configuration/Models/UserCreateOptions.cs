using System.ComponentModel.DataAnnotations;

namespace WURS.Business.Configuration.Models;

public class UserCreateOptions
{
    [Required(AllowEmptyStrings = false)]
    public required string Secret { get; init; }
};
