using System;
using System.Threading.Tasks;
using UserService.Controllers.ViewModels.RequestModels;
using UserService.Domains;

namespace UserService.Services
{
    public interface IUserService
    {
        public Task<User> GetUserById(Guid id);
        public Task<User> GetUserByEmail(string email);
        public Task<Guid> CreateUser(User user);
        public Task<Character> CreateCharacter(Guid userId, CreateCharacterRequestModel createCharacterRequestModel);
        public Task<Character> ReplaceCharacter(Guid userId, ReplaceCharacterRequestModel createCharacterRequestModel);
        public Task<User> ReplaceUser(User user);
        public Task<bool> DeleteCharacter(Guid userId, Guid characterId);
        public Task<bool> DeleteUser(Guid id);
        public Task<bool> HardDeleteUser(Guid id);
    }
}