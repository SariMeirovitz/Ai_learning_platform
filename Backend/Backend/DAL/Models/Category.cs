using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
