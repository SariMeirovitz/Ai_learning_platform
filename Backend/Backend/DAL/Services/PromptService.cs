using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class PromptService : IPrompt
    {
        private readonly AppDbContext _context;

        public PromptService(AppDbContext context)
        {
            _context = context;
        }

        public Prompt Create(Prompt entity)
        {
            _context.Prompts.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var prompt = GetById(id);
            if (prompt != null)
            {
                _context.Prompts.Remove(prompt);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Prompt> GetAll() => _context.Prompts.ToList();

        public Prompt GetById(int id) => _context.Prompts.Find(id);

        public Prompt Update(Prompt entity)
        {
            _context.Prompts.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public IEnumerable<Prompt> GetByUserId(int userId)
        {
            return _context.Prompts.Where(p => p.UserId == userId).ToList();
        }
    }
}
