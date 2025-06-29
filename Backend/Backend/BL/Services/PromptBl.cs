using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Services
{
    public class PromptBl : IPromptBl
    {
        private readonly IPrompt _promptDal;
        private readonly OpenAiService _openAiService;

        public PromptBl(IDal dal, OpenAiService openAiService)
        {
            _promptDal = dal.Prompt;
            _openAiService = openAiService;
        }

        public Prompt GetById(int id) => _promptDal.GetById(id);

        public IEnumerable<Prompt> GetAll() => _promptDal.GetAll();

        public Prompt Create(BLPrompt promptBl)
        {
            ValidateModel(promptBl);

            var prompt = new Prompt
            {
                UserId = promptBl.UserId,
                CategoryId = promptBl.CategoryId,
                SubCategoryId = promptBl.SubCategoryId,
                Prompt1 = promptBl.Prompt1,
                Response = promptBl.Response,
                Status = promptBl.Status
            };

            return _promptDal.Create(prompt);
        }

        public Prompt Update(BLPrompt promptBl)
        {
            ValidateModel(promptBl);

            var prompt = new Prompt
            {
                Id = promptBl.Id,
                UserId = promptBl.UserId,
                CategoryId = promptBl.CategoryId,
                SubCategoryId = promptBl.SubCategoryId,
                Prompt1 = promptBl.Prompt1,
                Response = promptBl.Response,
                Status = promptBl.Status
            };

            return _promptDal.Update(prompt);
        }

        public void Delete(int id) => _promptDal.Delete(id);

        private void ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            Validator.ValidateObject(model, context, validateAllProperties: true);
        }

        public IEnumerable<Prompt> GetByUserId(int userId)
        {
            return _promptDal.GetByUserId(userId);
        }

        public async Task<Prompt> SubmitPromptAsync(BLPrompt promptBl)
        {
            ValidateModel(promptBl);

            var formattedPrompt = FormatPrompt(promptBl);

            var response = await _openAiService.GetChatCompletionAsync(formattedPrompt);

            var promptToSave = new BLPrompt
            {
                UserId = promptBl.UserId,
                CategoryId = promptBl.CategoryId,
                SubCategoryId = promptBl.SubCategoryId,
                Prompt1 = promptBl.Prompt1,
                Response = response,
                Status = "completed"
            };

            return Create(promptToSave);
        }

        public string FormatPrompt(BLPrompt promptDto)
        {
            var category = promptDto.CategoryId?.ToString() ?? "Unknown Category";
            var subCategory = promptDto.SubCategoryId?.ToString() ?? "Unknown SubCategory";
            var userPrompt = promptDto.Prompt1;

            return $@"
You are an expert teacher specializing in {category} - {subCategory}.
Your task is to create engaging, clear, and structured educational lessons.

Student's request: ""{userPrompt}""

Instructions:
- If the student's request is a valid question or learning request about {category} - {subCategory}, provide a comprehensive lesson that includes:
  1. A clear explanation of the concept
  2. Real-world examples
  3. Key takeaways
  4. Suggested next steps for learning
- Make your response engaging, easy to understand, and well-structured.
- If the student's request is NOT a valid question or learning request about {category} - {subCategory}, politely respond:
  ""Your question is not valid for this topic. Please ask a question related to {category} - {subCategory}.""
";
        }
    }
}
