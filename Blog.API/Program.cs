
using Blog.API.Data;
using Blog.API.Repositories.Implementation;
using Blog.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddSingleton(RT.Comb.Provider.Sql);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BlogOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("BlogOrigins");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
