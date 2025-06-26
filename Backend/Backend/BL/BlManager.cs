using BL.Api;

namespace BL.Services
{
    public class BlManager : IBl
    {
        public IUserBl User { get; }
        public ICategoryBl Category { get; }
        public ISubCategoryBl SubCategory { get; }
        public IPromptBl Prompt { get; }

        public BlManager(IUserBl user, ICategoryBl category, ISubCategoryBl subCategory, IPromptBl prompt)
        {
            User = user;
            Category = category;
            SubCategory = subCategory;
            Prompt = prompt;
        }
    }
}
