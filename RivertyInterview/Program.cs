using RivertyInterview.Components;
using RivertyInterview.Models.Repository;
using RivertyInterview.Models.ServiceModel;
using RivertyInterview.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddSingleton<IRomanNumberService, RomanNumberService>();
builder.Services.AddTransient<IRomanCalculationHistoryRepository, RomanCalculationHistoryRepository>();
builder.Services.AddTransient<IRomanCalculationHistoryService, RomanCalculationHistoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
