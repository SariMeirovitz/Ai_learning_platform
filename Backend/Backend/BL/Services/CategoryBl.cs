using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Services
{
    public class CategoryBl : ICategoryBl
    {
        ICategory _categoryDal;

        public CategoryBl(IDal dal)
        {
            _categoryDal = dal.Category;
        }

        public Category GetById(int id) => _categoryDal.GetById(id);

        public IEnumerable<Category> GetAll() => _categoryDal.GetAll();

        public Category Create(BLCategory categoryBl)
        {
            ValidateModel(categoryBl);

            var category = new Category
            {
                Name = categoryBl.Name
            };

            return _categoryDal.Create(category);
        }

        public Category Update(BLCategory categoryBl)
        {
            ValidateModel(categoryBl);

            var category = new Category
            {
                Id = categoryBl.Id,
                Name = categoryBl.Name
            };

            return _categoryDal.Update(category);
        }

        public void Delete(int id) => _categoryDal.Delete(id);

        private void ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            Validator.ValidateObject(model, context, validateAllProperties: true);
        }
    }
}
