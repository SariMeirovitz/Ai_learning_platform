using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class BLPrompt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = "Prompt is required")]
        [StringLength(500, ErrorMessage = "Prompt can't be longer than 500 characters")]
        public string Prompt1 { get; set; } = string.Empty;

        public string? Response { get; set; }

        [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters")]
        public string? Status { get; set; }
    }
}
