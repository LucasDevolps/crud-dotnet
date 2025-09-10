using System.ComponentModel.DataAnnotations;

namespace EstoqueApi.DTOs
{
    public sealed class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public required string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public required string ConfirmPassword { get; set; }

    }
}
