using Microsoft.Extensions.Configuration;

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

try
{
    using HttpResponseMessage response = await client.GetAsync("health");
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"OK {client.BaseAddress}");
        return 0;
    }

    Console.WriteLine($"Virhe {(int)response.StatusCode} {client.BaseAddress}");
    return 1;
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Ei yhteyttä {client.BaseAddress}: {e.Message}");
    return 1;
}