using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class SubCategoryService : ISubCategory
    {
        private readonly AppDbContext _context;

        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public SubCategory Create(SubCategory entity)
        {
            _context.SubCategories.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var subCategory = GetById(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
                _context.SaveChanges();
            }
        }

        public IEnumerable<SubCategory> GetAll() => _context.SubCategories.ToList();

        public SubCategory GetById(int id) => _context.SubCategories.Find(id);

        public SubCategory Update(SubCategory entity)
        {
            _context.SubCategories.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
