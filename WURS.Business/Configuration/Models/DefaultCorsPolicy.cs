using System.ComponentModel.DataAnnotations;

namespace WURS.Business.Configuration.Models;

public class DefaultCorsPolicy
{
    public static string SectionName { get; } = nameof(DefaultCorsPolicy);
    [Required]
    public required string[] AllowedOrigins { get; init; }
    [Required]
    public required string[] AllowedMethods { get; init; }
    [Required]
    public required string[] AllowedHeaders { get; set; }
}
