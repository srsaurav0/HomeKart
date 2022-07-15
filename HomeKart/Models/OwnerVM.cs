using System.ComponentModel.DataAnnotations;

namespace HomeKart.Models
{
    public class OwnerVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Area { get; set; }
        [Required]
        public int No_of_Rooms { get; set; }
        [Required]
        public int Floor_No { get; set; }
        [Required]
        public int Rent_Amount { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
