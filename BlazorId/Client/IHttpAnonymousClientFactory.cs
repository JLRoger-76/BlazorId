namespace BlazorId.Client
{
    public interface IHttpAnonymousClientFactory
    {
        HttpClient HttpClient { get; }
    }
}
