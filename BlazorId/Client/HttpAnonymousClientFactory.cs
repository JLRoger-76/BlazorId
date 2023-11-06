namespace BlazorId.Client
{
    public class HttpAnonymousClientFactory : IHttpAnonymousClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpAnonymousClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public HttpClient HttpClient => httpClientFactory.CreateClient("BlazorId.ServerAPI.Anonymous");

    }
}
