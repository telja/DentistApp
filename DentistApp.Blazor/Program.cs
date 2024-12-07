using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DentistApp.Blazor;
using DentistApp.Blazor.Services.User;
using DentistApp.Blazor.Services.User.Impl;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5090/") });


builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();
