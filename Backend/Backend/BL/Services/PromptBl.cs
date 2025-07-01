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
1. **First**, analyze whether the student's request is clearly related to the topic of {category} - {subCategory}.
   - If the request is not relevant to this topic, respond **only** with:
     ""Your question is not valid for this topic. Please ask a question related to {category} - {subCategory}.""
   - Do **not** provide any lesson or explanation if the request is not relevant.

2. If the request **is** valid and related, provide a comprehensive lesson that includes:
   - A clear explanation of the concept
   - Real-world examples
   - Key takeaways
   - Suggested next steps for learning

3. Make your response engaging, easy to understand, and well-structured.
";
        }

    }
}
