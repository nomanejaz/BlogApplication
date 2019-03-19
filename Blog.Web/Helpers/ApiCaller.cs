using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Web.Helpers
{
    public class ApiCaller : IDisposable
    {
        private HttpClient Client { get; set; }
        private HttpResponseMessage Response { get; set; }

        public ApiCaller()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["Service_Url"])
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.Timeout = new TimeSpan(0, 5, 0);
        }

        public void Dispose()
        {
            try
            {
                if (Response != null)
                    Response.Dispose();
                if (Client != null)
                    Client.Dispose();
            }
            finally
            {
                Client = null;
                Response = null;
            }
        }

        public T Get<T>(string action, string controller)
        {
            AddAuthToken();
            var result = AsyncHelper.RunSync<T>(() => GetAsync<T>(action, controller));
            return result;
        }

        public T Post<T, R>(string action, string controller, R data)
        {
            AddAuthToken();
            var result = AsyncHelper.RunSync<T>(() => PostAsync<T, R>(action, controller, data));
            return result;
        }

        public async Task<T> GetAsync<T>(string action, string controller)
        {
            AddAuthToken();
            var methodUrl = string.Format("{0}/{1}", controller, action);
            Response = await Client.GetAsync(methodUrl);

            if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                HttpContext.Current.Response.Redirect("~/Account/Login");
            }

            if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                HttpContext.Current.Response.Redirect("~/Home/NotFound");
            }

            if (Response.IsSuccessStatusCode)
            {
                return await Response.Content.ReadAsAsync<T>();
            }
            else
                throw new Exception();
        }

        public async Task<T> GetAsync<T>(string action, string controller, string request)
        {
            AddAuthToken();
            var methodUrl = string.Format("{0}/{1}/{2}", controller, action, request);
            Response = await Client.GetAsync(methodUrl);

            if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                HttpContext.Current.Response.Redirect("~/Account/Login");
            }

            if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                HttpContext.Current.Response.Redirect("~/Home/NotFound");
            }

            if (Response.IsSuccessStatusCode)
            {
                return await Response.Content.ReadAsAsync<T>();
            }
            else
                throw new Exception();
        }

        public async Task<T> PostAsync<T, R>(string action, string controller, R data)
        {
            AddAuthToken();
            var methodUrl = string.Format("{0}/{1}", controller, action);
            Response = await Client.PostAsJsonAsync<R>(methodUrl, data);

            if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                HttpContext.Current.Response.Redirect("~/Account/Login");
            }

            if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                HttpContext.Current.Response.Redirect("~/Home/NotFound");
            }

            if (Response.IsSuccessStatusCode)
            {
                var response = await Response.Content.ReadAsAsync<T>();
                return response;
            }
            throw new Exception();
        }

        private void AddAuthToken()
        {
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", SecurityHelper.GetToken());
        }
    }
}