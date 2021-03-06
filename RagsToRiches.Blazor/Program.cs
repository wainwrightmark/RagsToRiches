using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using MudBlazor.Services;

namespace RagsToRiches.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddMudServices();
            builder.Services.AddFluxor(options =>
                options.ScanAssemblies(Assembly.GetAssembly(typeof(Program)), Assembly.GetAssembly(typeof(GameState))));


            await builder.Build().RunAsync();
        }
    }
}