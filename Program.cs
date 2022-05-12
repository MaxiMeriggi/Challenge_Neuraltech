using ChallengeNeuraltech.Automapper;
using ChallengeNeuraltech.Persistence.MySql;
using ChallengeNeuraltech.Repository.Actors;
using ChallengeNeuraltech.Repository.Genres;
using ChallengeNeuraltech.Repository.Images;
using ChallengeNeuraltech.Repository.Movies;
using ChallengeNeuraltech.Repository.Producers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(CustomProfile));
builder.Services.AddScoped<IActorsRepository, ActorsRepository>();
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<IProducersRepository, ProducersRepository>();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddMvcCore(opt =>
{
    opt.SuppressAsyncSuffixInActionNames = false;
});

var mySqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<MySqlContext>(options =>
{
    options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString));
});

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
