using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () =>
{
    return "/dnsnofun \n /testMI";
});

app.MapGet("/dnsnofun", () =>
{
    return "V1 - Wrong, DNS is very fun. \n";
});

app.MapGet("/testMI", () =>
{
    try
    {
        var credential = new DefaultAzureCredential();
        var armClient = new ArmClient(credential);
        var subscription = armClient.GetDefaultSubscription();
        var subName = subscription.Data.DisplayName;
        return "This app has authenticated to Subscription: " + subName + "\n";
    }
    catch(Exception e)
    {
        return e.ToString();
    }

});

app.Run();
