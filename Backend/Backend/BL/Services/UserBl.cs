using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Services
{
    public class UserBl : IUserBl
    {
        IUser _userDal;

        public UserBl(IDal dal)
        {
            _userDal = dal.User;
        }

        public User GetById(int id) => _userDal.GetById(id);

        public IEnumerable<User> GetAll() => _userDal.GetAll();

        public User Create(BLUser userBl)
        {
            ValidateModel(userBl);

            var user = new User
            {
                Name = userBl.Name,
                Phone = userBl.Phone
            };

            return _userDal.Create(user);
        }

        public User Update(BLUser userBl)
        {
            ValidateModel(userBl);

            var user = new User
            {
                Id = userBl.Id,
                Name = userBl.Name,
                Phone = userBl.Phone
            };

            return _userDal.Update(user);
        }

        public void Delete(int id) => _userDal.Delete(id);
        public async Task<User?> GetUserByNameAndPhone(string name, string phone)
        {
            return await _userDal.GetUserByNameAndPhone(name, phone);
        }
        public async Task<List<UserWithPromptsDto>> GetAllUsersWithPrompts()
        {
            var users = await _userDal.GetAllUsersWithPrompts();

            return users.Select(u => new UserWithPromptsDto
            {
                UserId = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                Prompts = u.Prompts.Select(p => new PromptDto
                {
                    Prompt = p.Prompt1,
                    Response = p.Response,
                    CreatedAt = p.CreatedAt ?? DateTime.MinValue
                }).ToList()
            }).ToList();
        }


        private void ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            Validator.ValidateObject(model, context, validateAllProperties: true);
        }
    }
}
