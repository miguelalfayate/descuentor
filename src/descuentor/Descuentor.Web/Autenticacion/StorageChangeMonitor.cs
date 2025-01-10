using Microsoft.JSInterop;

namespace Descuentor.Web.Autenticacion;

public class StorageChangeMonitor : IDisposable
{
    private readonly CustomAuthStateProvider _authStateProvider;
    private DotNetObjectReference<StorageChangeMonitor> _reference;
    private readonly IJSRuntime _jsRuntime;

    public StorageChangeMonitor(
        IJSRuntime jsRuntime,
        CustomAuthStateProvider authStateProvider)
    {
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
        _reference = DotNetObjectReference.Create(this);
    }

    public async Task InitializeAsync()
    {
        await _jsRuntime.InvokeVoidAsync("initializeStorageMonitor", _reference);
    }

    [JSInvokable]
    public async Task OnStorageChanged(string key, string newValue)
    {
        if (key == "access_token" || key == "refresh_token")
        {
            await _authStateProvider.CheckAndUpdateAuthenticationState();
        }
    }

    public void Dispose()
    {
        _reference?.Dispose();
    }
}