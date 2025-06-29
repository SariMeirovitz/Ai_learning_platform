using DAL.Models;
using System.Collections.Generic;
using BL.Models;

namespace BL.Api
{
    public interface IUserBl
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        User Create(BLUser user);
        User Update(BLUser user);
        void Delete(int id);
        Task<User?> GetUserByNameAndPhone(string name, string phone);
        Task<List<UserWithPromptsDto>> GetAllUsersWithPrompts();

    }
}
