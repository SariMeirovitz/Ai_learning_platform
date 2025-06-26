using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Prompt
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string Prompt1 { get; set; } = null!;

    public string? Response { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual SubCategory? SubCategory { get; set; }

    public virtual User User { get; set; } = null!;
}
