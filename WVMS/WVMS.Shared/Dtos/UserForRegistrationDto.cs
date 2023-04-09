using System.ComponentModel.DataAnnotations;

namespace WVMS.Shared.Dtos
{
    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        public string? Email { get; init; }

        public string? PhoneNumber { get; init; }

       // [Required(ErrorMessage = "Location is required")]
        public string? Location { get; init; }

        public ICollection<string>? Roles { get; init; }
    }
}
