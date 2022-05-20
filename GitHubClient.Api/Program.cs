using GitHubClient.Api.Interfaces;
using GitHubClient.Api.Models;
using GitHubClient.Api.Query;
using GitHubClient.Api.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddMediatR(typeof(Mediator)); //registering MediatR and all required dependencies
                                               //registering handlers
builder.Services.AddScoped<IRequestHandler<GetAllAuthorsQuery, List<string>>, GitHubQueryHandler>();
//builder.Services.AddScoped<IRequestHandler<GetAllRepositoriesQuery, List<Repository>>, GitHubQueryHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHttpClient();


builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();

//builder.Services.AddScoped<SingleInstanceFactory>(p => t => p.GetRequiredService(t));
//builder.Services.AddScoped<MultiInstanceFactory>(p => t => p.GetRequiredServices(t));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
