using BlazorId.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Win32;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorId.ServerAPI",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

//added
//service to anonymous access to api
builder.Services.AddHttpClient("BlazorId.ServerAPI.Anonymous",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorId.ServerAPI"));
// Register a new service for getting an Anonymous HttpClient
//added interface and class in blazor.client folder
builder.Services.AddScoped<IHttpAnonymousClientFactory, HttpAnonymousClientFactory>();

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
