using UserService.Domains;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace UserService.ViewModels.GooglePayload
{
    public static class PayloadExtension
    {
        public static User ToModel(this Payload payload)
        {
            return new (payload.GivenName, payload.FamilyName, payload.Email);
        }
    }
}