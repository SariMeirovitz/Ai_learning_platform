using DAL.Models;
using System.Collections.Generic;
using BL.Models;

namespace BL.Api
{
    public interface ISubCategoryBl
    {
        SubCategory GetById(int id);
        IEnumerable<SubCategory> GetAll();
        SubCategory Create(BLSubCategory subCategory);
        SubCategory Update(BLSubCategory subCategory);
        void Delete(int id);
    }
}
