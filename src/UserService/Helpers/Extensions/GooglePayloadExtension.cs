using UserService.Domains;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace UserService.Helpers.Extensions
{
    public static class GooglePayloadExtension
    {
        public static User ToModel(this Payload payload)
        {
            return new (payload.GivenName, payload.FamilyName, payload.Email);
        }
    }
}