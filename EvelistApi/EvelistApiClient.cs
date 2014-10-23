using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvelistApi.Results;

namespace EvelistApi
{
    public class EvelistApiClient : IEvelistApi
    {
        private const string Url = "http://evelist.ru/api.php";

        readonly JsonRPC rpc = new JsonRPC(Url);


        public async Task<bool> IsMidValid(string uid)
        {
            return (await rpc.InvokeMethod<List<bool>>("IsMidValid", "ApiController", uid)).FirstOrDefault();
        }

        public async Task<AuthResult> Auth(string login, string password)
        {
            return await rpc.InvokeMethod<AuthResult>("Auth", "ApiController", login, password);
        }

        public async Task<AuthResult> AuthBySecret(string secret)
        {
            return await rpc.InvokeMethod<AuthResult>("AuthBySecret", "ApiController", secret);
        }

        public async Task<AuthResult> AuthMid(string login, string password, string uid)
        {
            return await rpc.InvokeMethod<AuthResult>("AuthMid", "ApiController", login, password, uid);
        }

        public async Task<AuthResult> AuthBySecretMid(string secret, string uid)
        {
            return await rpc.InvokeMethod<AuthResult>("AuthBySecretMid", "ApiController", secret, uid);
        }

        public async Task<GetLinkedServicesResult> GetLinkedServices(string session)
        {
            return await rpc.InvokeMethod<GetLinkedServicesResult>("GetLinkedServiceList", "ApiController", session);
        }

        public async Task<GetEventListResult> GetEventList(string session)
        {
            return await rpc.InvokeMethod<GetEventListResult>("GetEventList", "ApiController", session);
        }

        public async Task<BaseResult> SendMessage(string session, string eventId, string message, ServicesArray services)
        {
            return await rpc.InvokeMethod<BaseResult>("SendMessage", "ApiController", session, eventId, message, services);
        }

        public async Task<BaseResult> SendPhoto(string session, string eventId, string name, ServicesArray services, string fileUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> SendVideo(string session, string eventId, string name, ServicesArray services, string fileUrl)
        {
            throw new NotImplementedException();
        }
    }
}
