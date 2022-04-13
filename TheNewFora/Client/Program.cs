
global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheNewFora.Client;
using TheNewFora.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IInterestsManager, InterestsManager>();
builder.Services.AddScoped<IApplicationUsersManager, ApplicationUsersManager>();
builder.Services.AddScoped<IThreadsManager, ThreadsManager>();
builder.Services.AddScoped<IMessagesManager, MessagesManager>();
builder.Services.AddScoped<IUserInterestsManager, UserInterestsManager>();


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
