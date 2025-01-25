using System.ComponentModel.DataAnnotations;

namespace Descuentor.WebAssembly.Modelos.Requests;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;
}