using System.ComponentModel.DataAnnotations;

namespace WURS.Business.Configuration.Models;

public class UserCreateOptions
{
    public static string SectionName { get; } = nameof(UserCreateOptions);

    [Required(AllowEmptyStrings = false)]
    public required string Secret { get; init; }
};
