using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;

namespace BL.Api
{
    public interface IPromptBl
    {
        Prompt GetById(int id);
        IEnumerable<Prompt> GetAll();
        Prompt Create(BLPrompt prompt);
        Prompt Update(BLPrompt prompt);
        void Delete(int id);
        IEnumerable<Prompt> GetByUserId(int userId);
        Task<Prompt> SubmitPromptAsync(BLPrompt promptBl);
    }
}
