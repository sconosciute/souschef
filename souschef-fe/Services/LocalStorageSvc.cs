using System.Text.Json;
using Microsoft.JSInterop;

namespace souschef_fe.Services;

public interface ILocalStorageService
{
    Task<T?> GetItem<T>(string key) where T : class;
    Task SetItem<T>(string key, T value);
    Task RemoveItem(string key);
}

public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService
{
    public async Task<T?> GetItem<T>(string key) where T : class
    {
        try
        {
            return await jsRuntime.InvokeAsync<T>("localStorage.getItem", key);
        }
        catch (JSException e) when (e.InnerException is JsonException)
        {
            return null;
        }
    }

    public async Task SetItem<T>(string key, T value)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }

    public async Task RemoveItem(string key)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}