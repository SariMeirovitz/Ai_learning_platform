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

        public PromptBl(IDal dal)
        {
            _promptDal = dal.Prompt;
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
    }
}
