using Microsoft.Extensions.Options;
using PaymentAPI.Models;
using PaymentAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register PaymentSettings from appsettings.json as an option
        builder.Services.Configure<PaymentSettings>(builder.Configuration.GetSection("PaymentSettings"));

        // Register IPaymentSettings using IOptions
        builder.Services.AddSingleton<IPaymentSettings>(sp =>
            sp.GetRequiredService<IOptions<PaymentSettings>>().Value);

        // Register PaymentService
        builder.Services.AddSingleton<PaymentService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(options => 
        options.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader());

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}