using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Services
{
    public class SubCategoryBl : ISubCategoryBl
    {
        private readonly ISubCategory _subCategoryDal;

        public SubCategoryBl(IDal dal)
        {
            _subCategoryDal = dal.SubCategory;
        }

        public SubCategory GetById(int id) => _subCategoryDal.GetById(id);

        public IEnumerable<SubCategory> GetAll() => _subCategoryDal.GetAll();

        public SubCategory Create(BLSubCategory subCategoryBl)
        {
            ValidateModel(subCategoryBl);

            var subCategory = new SubCategory
            {
                Name = subCategoryBl.Name,
                CategoryId = subCategoryBl.CategoryId
            };

            return _subCategoryDal.Create(subCategory);
        }

        public SubCategory Update(BLSubCategory subCategoryBl)
        {
            ValidateModel(subCategoryBl);

            var subCategory = new SubCategory
            {
                Id = subCategoryBl.Id,
                Name = subCategoryBl.Name,
                CategoryId = subCategoryBl.CategoryId
            };

            return _subCategoryDal.Update(subCategory);
        }

        public void Delete(int id) => _subCategoryDal.Delete(id);

        private void ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            Validator.ValidateObject(model, context, validateAllProperties: true);
        }
    }
}
