using CurrencyData.Components;
using CurrencyData.Services;
using CurrencyData.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=currency.db"));
builder.Services.AddHttpClient<CurrencyService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CurrencyApi:BaseUrl"]!);
});
builder.Services.AddScoped<CurrencyService>();
builder.Services.AddServerSideBlazor()
       .AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddScoped<DialogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
