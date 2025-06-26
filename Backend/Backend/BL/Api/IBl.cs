using BL.Api;

namespace BL.Api
{
    public interface IBl
    {
        IUserBl User { get; }
        ICategoryBl Category { get; }
        ISubCategoryBl SubCategory { get; }
        IPromptBl Prompt { get; }
    }
}
