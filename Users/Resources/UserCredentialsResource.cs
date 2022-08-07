using System.ComponentModel.DataAnnotations;

namespace Users.Resources
{
    public struct UserCredentialsResource
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

