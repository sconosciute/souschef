using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using souschef_core.Services;

namespace souschef_fe.Services;

public interface IHttpService
{
    Task<T?> Get<T>(string uri);
    Task<TResponse?> Post<TRequest, TResponse>(string uri, TRequest value);
}

public class HttpService(HttpClient client, NavigationManager nav, ILocalStorageService localStore, IConfiguration conf)
    : IHttpService
{
    public async Task<T?> Get<T>(string uri)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(req);
    }

    public async Task<TResponse?> Post<TRequest, TResponse>(string uri, TRequest value)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, uri);
        req.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequest<TResponse>(req);
    }

    private async Task<T?> SendRequest<T>(HttpRequestMessage req)
    {
        Validate.Begin()
            .IsNotNull(req.RequestUri, nameof(req.RequestUri))
            .Check()
            .IsValidUri(req.RequestUri!)
            .Check();
        var token = await localStore.GetItem<string>("token");
        if (string.IsNullOrEmpty(token))
        {
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        using var res = await client.SendAsync(req);
        res.EnsureSuccessStatusCode();
        
        return await res.Content.ReadFromJsonAsync<T>();
    }
}

public static class HttpValidationExtensions
{
    public static Validation? IsValidUri(this Validation? v, Uri uri)
    {
        return uri.IsAbsoluteUri
            ? (v ?? new Validation()).AddException(
                new UriFormatException(uri + "not valid for this context"))
            : null;
    }
}