using System;
using System.Collections.Generic;

namespace BL.Models
{
    public class UserWithPromptsDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public List<PromptDto> Prompts { get; set; } = new();
    }

    public class PromptDto
    {
        public string Prompt { get; set; } = "";
        public string Response { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
