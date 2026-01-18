using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(o => o.EnableEndpointRouting = false);

var app = builder.Build();
app.UseStaticFiles();
app.UseMvc();
app.Run();
