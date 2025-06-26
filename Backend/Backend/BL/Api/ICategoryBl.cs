using DAL.Models;
using System.Collections.Generic;
using BL.Models;

namespace BL.Api
{
    public interface ICategoryBl
    {
        Category GetById(int id);
        IEnumerable<Category> GetAll();
        Category Create(BLCategory category);
        Category Update(BLCategory category);
        void Delete(int id);
    }
}
