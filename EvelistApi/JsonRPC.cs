using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EvelistApi
{
    public class JsonRPCResult<T>
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public T result { get; set; }
        public object error { get; set; }
    }

    public class JsonRPC
    {

        private readonly string Url;

        public JsonRPC(string url)
        {
            Url = url;
        }

        class JsonRpcRequest
        {
            public string jsonrpc { get; set; }
            public int id { get; set; }
            public string method { get; set; }
            public List<object> @params { get; set; }
        }

        public async Task<string> InvokeMethod(string method, string extension, params object[] parameters)
        {


            //var url = "http://evelist.ru/api.php";
            //var url = "http://evelist.ru/";
            //var url = "http://www.raboof.com/projects/jayrock/demo.ashx"; 

            //webRequest.Credentials = new NetworkCredential("evelist", "betademo");

            var req = new JsonRpcRequest
            {
                id = 1,
                jsonrpc = "2.0",
                method = extension + "." + method,
                @params = parameters.ToList()
            };

            var s = JsonConvert.SerializeObject(req);

            return await InvokeString(Url, s);

        }

        public async Task<T> InvokeMethod<T>(string method, string extension, params object[] parameters)
        {
            var tr = await InvokeMethod(method, extension, parameters);
            var result = JsonConvert.DeserializeObject<JsonRPCResult<T>>(tr);
            if (result.error != null)
                throw new Exception(result.error.ToString());
            return result.result;


        }


        private static async Task<string> InvokeString(string url, string request)
        {

            var webRequest = WebRequest.Create(new Uri(url));

            //webRequest.Headers["x-RPC-Auth-Session"] = "";
            webRequest.ContentType = "application/json";

            webRequest.Method = "POST";

            var byteArray = Encoding.UTF8.GetBytes(request);
            //webRequest. = byteArray.Length;


            using (var stream = await webRequest.GetRequestStreamAsync())
            {
                stream.Write(byteArray, 0, byteArray.Length);
            }


            using (var stream = await webRequest.GetResponseAsync())
            {
                using (var sr = new StreamReader(stream.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }

        }

    }
}
