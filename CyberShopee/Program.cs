
using CyberShopee.Data;
using CyberShopee.Repository.DAO;
using CyberShopee.Repository.Service;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(Options =>
            Options.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));

            // Dependency Injection
            builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepo, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepo, OrderRepository>();
            builder.Services.AddScoped<IOrderDetailsRepo, OrderDetailsRepository>();
            builder.Services.AddScoped<IProductRepo, ProductRepository>();
            builder.Services.AddScoped<IShoppingCartRepo, ShoppingCartRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}
