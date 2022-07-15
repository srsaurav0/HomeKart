using System.ComponentModel.DataAnnotations;

namespace HomeKart.Models
{
    public class AdminVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
