using System;
using CoreLib.Exceptions;

namespace UserService.Exceptions
{
    public class UserNotFound : NotFound
    {
        public UserNotFound(string email) : base(404, $"User not found with email: {email}") { }
    }
    public class CharacterNotFound : NotFound
    {
        public CharacterNotFound(Guid characterId) : base(404, $"Character not found with id: {characterId.ToString()}") { }
    }
}