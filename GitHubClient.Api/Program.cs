using GitHubClient.Api.Interfaces;
using GitHubClient.Api.Query;
using GitHubClient.Api.Services;
using MediatR;
using GitHubClient.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddMediatR(typeof(Mediator)); //registering MediatR and all required dependencies
                                               //registering handlers
builder.Services.AddScoped<IRequestHandler<GetAllAuthorsQuery, List<string>>, GitHubQueryHandler>();
builder.Services.AddControllers();
builder.Services.AddApiVersioningConfigured();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();

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
