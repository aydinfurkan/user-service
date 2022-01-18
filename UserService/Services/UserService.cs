using System;
using System.Threading.Tasks;
using UserService.Controllers.ViewModels.RequestModels;
using UserService.Domains;
using UserService.Exceptions;
using UserService.Repositories;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _repository.GetUserById(id);
            if (user == null)
                throw new UserNotFound(id.ToString());
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user == null)
                throw new UserNotFound(email);
            return user;
        }

        public async Task<Guid> CreateUser(User user)
        {
            if (await _repository.GetUserByEmail(user.Email) != null)
                throw new UserConflict(user.Email);
            return await _repository.CreateUser(user);
        }

        public async Task<Character> CreateCharacter(Guid userId, CreateCharacterRequestModel requestModel)
        {
            var user = await GetUserById(userId);
            var newCharacter = new Character(requestModel.CharacterName, requestModel.CharacterClass.ToLower());
            user.AddCharacter(newCharacter);
            await _repository.UpdateUser(user, (x => x.CharacterList, user.CharacterList));
            return newCharacter;
        }

        public async Task<Character> ReplaceCharacter(Guid userId, ReplaceCharacterRequestModel requestModel)
        {
            var user = await GetUserById(userId);
            var changedCharacter = user.UpdateCharacter(requestModel.CharacterId, requestModel.Position.ToModel(), requestModel.Health);
            await _repository.UpdateUser(user, (x => x.CharacterList, user.CharacterList));
            return changedCharacter;
        }

        public async Task<User> ReplaceUser(User user)
        {
            var replacedUser = await _repository.ReplaceUser(user);
            if (replacedUser == null)
                throw new UserNotFound(user.Id.ToString());
            return replacedUser;
        }

        public async Task<bool> DeleteCharacter(Guid userId, Guid characterId)
        {
            var user = await GetUserById(userId);
            user.DeleteCharacter(characterId);
            await _repository.UpdateUser(user, (x => x.CharacterList, user.CharacterList));
            return true;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var result = await _repository.DeleteUser(id);
            if (!result)
                throw new UserNotFound(id.ToString());
            return true;
        }

        public async Task<bool> HardDeleteUser(Guid id)
        {
            var result = await _repository.HardDeleteUser(id);
            if (!result)
                throw new UserNotFound(id.ToString());
            return true;
        }

    }
}