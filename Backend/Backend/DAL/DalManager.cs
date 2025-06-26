using DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL.Services;
using DAL.Models;


namespace DAL
{
    public class DalManager : IDal
    {
        public IUser User { get; }
        public ICategory Category { get; }
        public ISubCategory SubCategory { get; }
        public IPrompt Prompt { get; }

        public DalManager(IUser user, ICategory category, ISubCategory subCategory, IPrompt prompt)
        {
            User = user;
            Category = category;
            SubCategory = subCategory;
            Prompt = prompt;
        }
    }
}
