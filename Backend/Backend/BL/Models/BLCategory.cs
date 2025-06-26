using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class BLCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
