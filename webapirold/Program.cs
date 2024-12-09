using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Domain.Models;
using Domain.Interfaces;
using DataAccess.Wrapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using BusinessLogic.Sevices;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);





            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IAirportService, AirportService>();
            builder.Services.AddScoped<IBaggageService, BaggageService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IChangerService, ChangerService>();
            builder.Services.AddScoped<ICharterService, CharterService>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IPassengerService, PassengerService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ISpecialService, SpecialService2>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();

            }));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Интернет-магазин API",
                    Description = "Краткое описание вашего API",
                    Contact = new OpenApiContact
                    {
                        Name = "Пример контакта",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Пример лицензии",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });


            builder.Services.AddDbContext<RolandContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionString"]));





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
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();



        }
    }
}