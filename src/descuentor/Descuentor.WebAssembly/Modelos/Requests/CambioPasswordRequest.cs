using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Descuentor.WebAssembly.Modelos.Requests;

public class CambioPasswordRequest
{
    public string? NewEmail { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    [JsonIgnore]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; } = null!;
}