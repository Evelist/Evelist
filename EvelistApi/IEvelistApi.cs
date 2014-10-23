using System.Threading.Tasks;
using EvelistApi.Results;

namespace EvelistApi
{
    interface IEvelistApi
    {
        Task<bool> IsMidValid(string uid);

        Task<AuthResult> Auth(string login, string password);
        Task<AuthResult> AuthBySecret(string secret);

        Task<AuthResult> AuthMid(string login, string password, string uid);
        Task<AuthResult> AuthBySecretMid(string secret, string uid);

        Task<GetLinkedServicesResult> GetLinkedServices(string session);

        Task<GetEventListResult> GetEventList(string session);

        Task<BaseResult> SendMessage(string session, string eventId, string message, ServicesArray services);

        Task<BaseResult> SendPhoto(string session, string eventId, string name, ServicesArray services, string fileUrl);

        Task<BaseResult> SendVideo(string session, string eventId, string name, ServicesArray services, string fileUrl);
    }
}
