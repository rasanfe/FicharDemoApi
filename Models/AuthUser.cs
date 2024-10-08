using System.ComponentModel.DataAnnotations;

namespace FicharApi.Models
{
    public class AuthUser
    {
        [Required]
        public string? Username { get; set; }

        [Required, DataType(DataType.Password)]
       
        public string? Password { get; set; }
    }
}
