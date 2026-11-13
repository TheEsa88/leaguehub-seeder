using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

JsonSerializerOptions jsonOptions = new()
{
    PropertyNameCaseInsensitive = true
};

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.local.json", optional: true)
    .Build();

string baseUrl = config["ApiBaseUrl"] ?? throw new InvalidOperationException("ApiBaseUrl puuttuu.");

using HttpClient client = new()
{
    BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/")
};

Console.WriteLine($"Kohde {client.BaseAddress}");
