using System.ComponentModel.DataAnnotations;

namespace EstoqueApi.Models
{
    public sealed class User
    {
        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public User() => Uuid = Guid.NewGuid();
    }
}
