using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserService.Domains;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(Guid id, bool isDeleted = false);
        public Task<User> GetUserByEmail(string email, bool isDeleted = false);
        public Task<Tuple<List<User>, long>> GetUsers(Expression<Func<User, bool>> expression, int from, int size, SortDefinition<User> sortDefinition);
        public Task<Guid> CreateUser(User user);
        public Task<User> ReplaceUser(User user);
        public Task<bool> HardDeleteUser(Guid id);
        public Task<bool> DeleteUser(Guid id);
    }
}