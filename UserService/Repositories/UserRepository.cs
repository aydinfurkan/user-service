using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreLib.Mongo.Context;
using CoreLib.Mongo.Repository;
using MongoDB.Driver;
using UserService.Domains;
using UserService.Exceptions;

namespace UserService.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IContext context) : base(context)
        {
            
        }
        public async Task<User> GetUserById(Guid id, bool isDeleted = false)
        {
            return await FindOneAsync(c => c.Id.Equals(id) && c.IsDeleted == isDeleted);
        }
        public async Task<User> GetUserByEmail(string email, bool isDeleted = false)
        {
            return await FindOneAsync(c => c.Email.Equals(email) && c.IsDeleted == isDeleted);
        }

        public async Task<Tuple<List<User>, long>> GetUsers(Expression<Func<User, bool>> expression, int from, int size, 
            SortDefinition<User> sortDefinition)
        {
            return await FindManyAsync(expression,from,size,sortDefinition);
        }

        public async Task<Guid> CreateUser(User user)
        {
            return (Guid) await InsertOneAsync(user);
        }

        public async Task<User> ReplaceUser(User user)
        {
            var result = await ReplaceOneAsync(c => c.Id == user.Id  && !c.IsDeleted, user);
            
            if (result > 0)
                return user;

            throw new UserNotFound(user.Id.ToString());
        }

        public async Task<bool> HardDeleteUser(Guid id)
        {
            var result = await DeleteOneAsync(c => c.Id.Equals(id));
            
            return result > 0;
        }
        
        public async Task<bool> DeleteUser(Guid id)
        {
            var result = await UpdateOneAsync(c => c.Id.Equals(id),
                (c => c.DeletedAt, DateTime.Now),(c => c.IsDeleted, true));
            
            return result > 0;
        }

    }
}